using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ExcelDataReader;
using System.Data;
using System.IO;
using System;

public class DataConverter
{

    [MenuItem("Test/ReadFromExcel")]
    public static void ReadFromExcel()
    {
        Debug.Log("Test");
        System.Data.DataSet dataset;

        string filePath = "Assets/Excel/Data.xlsx";
        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                dataset = reader.AsDataSet();
            }
        }

        Debug.Log($"TableCount :{dataset.Tables.Count}");

        string assetPath = "Assets/Data/data.asset";

        var gameData = AssetDatabase.LoadAssetAtPath<GameData>(assetPath);

        if (gameData == null)
        {
            gameData = ScriptableObject.CreateInstance<GameData>();

            AssetDatabase.CreateAsset(gameData, assetPath);
        }
        else
        {
            EditorUtility.SetDirty(gameData);
        }

        var gtype = typeof(GameData);
        var dtype = typeof(DataConverter);
        var gfis = gtype.GetFields();
        var dmi = dtype.GetMethod("MakeListFromDataset", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        foreach (var f in gfis)
        {
            var ftype = f.FieldType;
            if (ftype.IsGenericType == false || ftype.GetGenericTypeDefinition() != typeof(List<>))
            {
                continue;
            }
            Debug.Log(f.Name);
            var nmi = dmi.MakeGenericMethod(ftype.GenericTypeArguments[0]);
            var res = nmi.Invoke(null, new object[] { dataset, f.Name });
            f.SetValue(gameData, res);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static List<T> MakeListFromDataset<T>(DataSet dataset, string tableName) where T : new()
    {
        List<T> list = new List<T>();
        var table = FindTable(dataset, tableName);
        if (table == null)
        {
            throw new Exception($"{tableName}�� �����ϴ�.");
        }
        var itemType = typeof(T);
        for (int row = 1; row < table.Rows.Count; row++)
        {
            var item = new T();
            list.Add(item);
            for (int col = 0; col < table.Columns.Count; col++)
            {
                var Fieldname = table.Rows[0][col] as string;
                var fi = itemType.GetField(Fieldname);
                if (fi == null)
                {
                    Debug.LogError($"{itemType.Name}�� {Fieldname}��(��) �����ϴ�.");
                }
                var value = table.Rows[row][col];
                var fieldType = fi.FieldType;

                if (fieldType.GetInterface("IFillFromStr") != null)
                {
                    var obj = System.Activator.CreateInstance(fieldType) as IFillFromStr;
                    obj.FillFromString(value as string);
                    fi.SetValue(item, obj);
                }
                else if (fieldType.IsEnum)
                {
                    fi.SetValue(item, Enum.Parse(fi.FieldType, value as string));
                }
                else
                {
                    try{
                        fi.SetValue(item, Convert.ChangeType(value, fi.FieldType));
                    }catch
                    {
                        Debug.Log($"item : {item} // FieldType : {fi.FieldType.Name} // FI : {fi.Name}");
                    }
                }

            }
        }
        return list;
    }

    private static DataTable FindTable(DataSet dataset, string tablename)
    {
        DataTable table = null;
        foreach (DataTable t in dataset.Tables)
        {
            if (t.TableName == tablename)
            {
                table = t;
                break;
            }
        }
        return table;
    }
}