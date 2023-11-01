using UnityEngine;
public class KeySetting : MonoBehaviour
{
    private GameObject ActiveKeySet;
    public bool KySetSi; //Key Setting Situation

    SoundSetting soundSetting;
    DataSetting dataSetting;
    ScreenSetting screenSetting;
    LocationSetting locationSetting;
    void Awake()
    {
        KySetSi = false;
        dataSetting = GameObject.Find("DataSetting").GetComponent<DataSetting>();
        soundSetting = GameObject.Find("SoundSetting").GetComponent<SoundSetting>();
        locationSetting = GameObject.Find("LocationSetting").GetComponent<LocationSetting>();
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
        locationSetting.LcSetSi = false;
        dataSetting.DtSetSi = false;
    }
}
