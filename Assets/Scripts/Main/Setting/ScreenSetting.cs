using UnityEngine;

public class ScreenSetting : MonoBehaviour
{
    private GameObject ActiveScreenSet;
    public bool ScSetSi; //Screen Setting Situation

    SoundSetting soundSetting;
    KeySetting keySetting;
    DataSetting dataSetting;
    LocationSetting locationSetting;
    void Awake()
    {
        ScSetSi = false;
        dataSetting = GameObject.Find("DataSetting").GetComponent<DataSetting>();
        soundSetting = GameObject.Find("SoundSetting").GetComponent<SoundSetting>();
        keySetting = GameObject.Find("KeySetting").GetComponent<KeySetting>();
        locationSetting = GameObject.Find("LocationSetting").GetComponent<LocationSetting>();
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
        locationSetting.LcSetSi = false;
        dataSetting.DtSetSi = false;
    }
}
