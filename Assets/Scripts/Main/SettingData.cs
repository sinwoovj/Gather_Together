using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SettingData
{
    // Key Value
    public KeyCode UpKeySettingValue = KeyCode.W;
    public KeyCode DownKeySettingValue = KeyCode.S;
    public KeyCode LeftKeySettingValue = KeyCode.A;
    public KeyCode RightKeySettingValue = KeyCode.D;
    public KeyCode InteractionKeySettingValue = KeyCode.F;

    public IEnumerable<(KeyCode,KeyAction)> EachKey()
    {
        yield return (UpKeySettingValue,KeyAction.UP);
        yield return (DownKeySettingValue,KeyAction.DOWN);  
        yield return (LeftKeySettingValue,KeyAction.LEFT);
        yield return (RightKeySettingValue,KeyAction.RIGHT);
        yield return (InteractionKeySettingValue,KeyAction.INTERACTION);

    }

   


    public static SettingData LoadSettingData() 
    { 

        var settingFilePath = GetSettinghFilePath();
        Debug.Log("SettingFilePath " + settingFilePath);

        if (System.IO.File.Exists(settingFilePath))
        {
            var json = System.IO.File.ReadAllText(settingFilePath);

            return JsonConvert.DeserializeObject<SettingData>(json);
            //JsonUtility.FromJson(json, typeof(SettingData)) as SettingData;
        }

        return new SettingData();
    }

    public void SaveSettingData()
    {
        string settingFilePath = GetSettinghFilePath();
        var json = JsonConvert.SerializeObject(this);
            // JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(settingFilePath, json);
    }

    private static string GetSettinghFilePath()
    {
        return System.IO.Path.Join(Application.persistentDataPath, "Setting.json");
    }
}
