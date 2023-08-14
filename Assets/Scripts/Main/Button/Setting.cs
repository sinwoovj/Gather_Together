using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Setting : MonoBehaviour
{
    private GameObject ActiveSetting;
    public bool SetSi; //Setting Situation
    void Awake()
    {
        SetSi = false;
        ActiveSetting = GameObject.Find("SettingInterface");
    }

    void Update()
    {
        if (SetSi == true)
        {
            ActiveSetting.SetActive(true);
        }

        else if (SetSi == false)
        {
            ActiveSetting.SetActive(false);
        }
    }
    public void OpenSetting()
    {
        SetSi = true;
        print("Open Setting");
    }
}
