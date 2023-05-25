using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundSetting : MonoBehaviour
{
    GameObject ActiveSoundSet;
    public bool SdSetSi; //Sound Setting Situation << 사운드 설정이 기본값

    ScreenSetting screenSetting;
    KeySetting keySetting;
    void Awake()
    {
        SdSetSi = true;
        screenSetting = GameObject.Find("ScreenSetting").GetComponent<ScreenSetting>();
        keySetting = GameObject.Find("KeySetting").GetComponent<KeySetting>();
    }
    void Start()
    {
        ActiveSoundSet = GameObject.Find("Sound");
    }
    void Update()
    {
        if (SdSetSi == true)
        {
            ActiveSoundSet.SetActive(true);
        }

        else if (SdSetSi == false)
        {
            ActiveSoundSet.SetActive(false);
        }
    }
    public void OpenSoundSetting()
    {
        SdSetSi = true;
        screenSetting.ScSetSi = false; //ScreenSetting 화면을 꺼준다.
        keySetting.KySetSi = false;
        print("Open Sound Setting");
    }
}
