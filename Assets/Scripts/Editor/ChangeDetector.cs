using UnityEngine;
using UnityEditor;
using Codice.Client.Commands.Matcher;

class ChangeDetector : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
    {
        foreach (string str in importedAssets)
        {
            Debug.Log("Reimported Asset: " + str);

            if (str == "Assets/Excel/Data.xlsx")
            {
                DataConverter.ReadFromExcel();
            }

        }
        foreach (string str in deletedAssets)
        {
            Debug.Log("Deleted Asset: " + str);
        }

        for (int i = 0; i < movedAssets.Length; i++)
        {
            Debug.Log("Moved Asset: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
        }

        if (didDomainReload)
        {
            Debug.Log("Domain has been reloaded");
        }
    }
}