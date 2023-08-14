using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSetting : MonoBehaviour
{
    private GameObject ActiveScreenSet;
    public bool ScSetSi; //Screen Setting Situation

    SoundSetting soundSetting;
    KeySetting keySetting;
    void Awake()
    {
        ScSetSi = false;
        soundSetting = GameObject.Find("SoundSetting").GetComponent<SoundSetting>();
        keySetting = GameObject.Find("KeySetting").GetComponent<KeySetting>();
    }

    void Start()
    {
        ActiveScreenSet = GameObject.Find("Screen");
    }
    void Update()
    {
        if (ScSetSi == true)
        {
            ActiveScreenSet.SetActive(true);
        }

        else if(ScSetSi == false)
        {
            ActiveScreenSet.SetActive(false);
        }
    }
    public void OpenScreenSetting()
    {
        ScSetSi = true;
        soundSetting.SdSetSi = false; //SoundSetting 화면을 꺼준다.
        keySetting.KySetSi = false;
        print("Open Screen Setting");
    }
}
