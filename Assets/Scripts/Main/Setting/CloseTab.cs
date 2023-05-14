using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CloseTab : MonoBehaviour
{
    Setting setting;
    void Awake()
    {
        setting = GameObject.Find("SettingButton").GetComponent<Setting>();
    }
    public void CloseSetting()
    {
        setting.SetSi = false;
        print("Close Setting");
    }
}
