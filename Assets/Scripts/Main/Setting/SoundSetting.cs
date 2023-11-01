using UnityEngine;

public class SoundSetting : MonoBehaviour
{
    GameObject ActiveSoundSet;
    public bool SdSetSi; //Sound Setting Situation << 사운드 설정이 기본값

    KeySetting keySetting;
    DataSetting dataSetting;
    ScreenSetting screenSetting;
    LocationSetting locationSetting;
    void Awake()
    {
        SdSetSi = false;
        dataSetting = GameObject.Find("DataSetting").GetComponent<DataSetting>();
        keySetting = GameObject.Find("KeySetting").GetComponent<KeySetting>();
        locationSetting = GameObject.Find("LocationSetting").GetComponent<LocationSetting>();
        screenSetting = GameObject.Find("ScreenSetting").GetComponent<ScreenSetting>();
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
        keySetting.KySetSi = false;
        screenSetting.ScSetSi = false;
        locationSetting.LcSetSi = false;
        dataSetting.DtSetSi = false;
    }
}
