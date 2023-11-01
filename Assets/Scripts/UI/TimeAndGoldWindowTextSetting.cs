using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DI;

public class TimeAndGoldWindowTextSetting : DIMono
{
    [Inject]
    UserData userData;

    public Image TimeAndGoldWindowWeather;
    public Text DateText;
    public Text TimeText;
    public Text GoldText;

    private void Awake()
    {
        TimeAndGoldWindowWeather = this.transform.GetChild(0).GetComponent<Image>();
        DateText = this.transform.GetChild(1).GetComponent<Text>();
        TimeText = this.transform.GetChild(2).GetComponent<Text>();
        GoldText = this.transform.GetChild(3).GetComponent<Text>();
    }

    protected override void Initialize()
    {
        
    }

    void Update()
    {

    }
}
