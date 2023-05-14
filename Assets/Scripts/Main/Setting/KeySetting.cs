using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeySetting : MonoBehaviour
{
    private GameObject ActiveKeySet;
    public bool KySetSi; //Key Setting Situation

    SoundSetting soundSetting;
    ScreenSetting screenSetting;
    void Awake()
    {
        KySetSi = false;
        soundSetting = GameObject.Find("SoundSetting").GetComponent<SoundSetting>();
        screenSetting = GameObject.Find("ScreenSetting").GetComponent<ScreenSetting>();
    }

    void Start()
    {
        ActiveKeySet = GameObject.Find("Key");
    }
    void Update()
    {
        if (KySetSi == true)
        {
            ActiveKeySet.SetActive(true);
        }

        else if (KySetSi == false)
        {
            ActiveKeySet.SetActive(false);
        }
    }
    public void OpenKeySetting()
    {
        KySetSi = true;
        soundSetting.SdSetSi = false; //SoundSetting 화면을 꺼준다.
        screenSetting.ScSetSi = false;
        print("Open Key Setting");
    }
}
