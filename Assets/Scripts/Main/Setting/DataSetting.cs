using UnityEngine;

public class DataSetting : MonoBehaviour
{
    private GameObject ActiveScreenSet;
    public bool DtSetSi; //Data Setting Situation

    SoundSetting soundSetting;
    KeySetting keySetting;
    LocationSetting locationSetting;
    ScreenSetting screenSetting;
    void Awake()
    {
        DtSetSi = false;
        soundSetting = GameObject.Find("SoundSetting").GetComponent<SoundSetting>();
        keySetting = GameObject.Find("KeySetting").GetComponent<KeySetting>();
        locationSetting = GameObject.Find("LocationSetting").GetComponent<LocationSetting>();
        screenSetting = GameObject.Find("ScreenSetting").GetComponent<ScreenSetting>();
    }

    void Start()
    {
        ActiveScreenSet = GameObject.Find("Data");
    }
    void Update()
    {
        if (DtSetSi == true)
        {
            ActiveScreenSet.SetActive(true);
        }

        else if (DtSetSi == false)
        {
            ActiveScreenSet.SetActive(false);
        }
    }
    public void OpenDataSetting()
    {
        DtSetSi = true;
        soundSetting.SdSetSi = false; //SoundSetting 화면을 꺼준다.
        keySetting.KySetSi = false;
        screenSetting.ScSetSi = false;
        locationSetting.LcSetSi = false;
    }
}
