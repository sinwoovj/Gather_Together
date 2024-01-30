using UnityEngine;

public class LocationSetting : MonoBehaviour
{
    private GameObject ActiveScreenSet;
    public bool LcSetSi; //Location Setting Situation

    SoundSetting soundSetting;
    KeySetting keySetting;
    DataSetting dataSetting;
    ScreenSetting screenSetting;
    void Awake()
    {
        LcSetSi = true;
        soundSetting = GameObject.Find("SoundSetting").GetComponent<SoundSetting>();
        keySetting = GameObject.Find("KeySetting").GetComponent<KeySetting>();
        dataSetting = GameObject.Find("DataSetting").GetComponent <DataSetting>();
        screenSetting = GameObject.Find("ScreenSetting").GetComponent<ScreenSetting>();
    }

    void Start()
    {
        ActiveScreenSet = GameObject.Find("Location");
    }
    void Update()
    {
        if (LcSetSi == true)
        {
            ActiveScreenSet.SetActive(true);
        }

        else if (LcSetSi == false)
        {
            ActiveScreenSet.SetActive(false);
        }
    }
    public void OpenLocationSetting()
    {
        LcSetSi = true;
        soundSetting.SdSetSi = false; //SoundSetting 화면을 꺼준다.
        keySetting.KySetSi = false;
        screenSetting.ScSetSi = false;
        dataSetting.DtSetSi = false;
    }
}
