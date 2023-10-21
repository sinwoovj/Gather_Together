using DI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public enum KeyAction{ UP, DOWN, LEFT, RIGHT, INTERACTION, KEYCOUNT}

//열거형의 실제 사용하는 값의 개수와 같음.

public class KeyManager : DIMono
{

    [Inject]
    SettingData settingData;

    public Text[] txt; //Text 변수를 배열로 선언
    public GameObject overlapText;

    protected override void Initialize()
    {
        UpdateKeyUI();
        Color overlapTextAlpha = overlapText.GetComponent<Text>().color;
        overlapTextAlpha.a = 0f;
        overlapText.GetComponent<Text>().color = overlapTextAlpha;
    }

    public void UpdateKeyUI()
    {
        txt[0].text = settingData.UpKeySettingValue.ToString();
        txt[1].text = settingData.DownKeySettingValue.ToString();
        txt[2].text = settingData.LeftKeySettingValue.ToString();
        txt[3].text = settingData.RightKeySettingValue.ToString();
        txt[4].text = settingData.InteractionKeySettingValue.ToString();
    }

    private KeyCode GetPressedKey()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                return key;
            }
        }
        return KeyCode.None;
    }

    IEnumerator ChageKeyIE(KeyAction action)
    {
        while (Input.anyKeyDown==false)
        {
            yield return null;
        }

        var keyCode = GetPressedKey();
        bool isKeyInSetting = false;
        KeyAction overlapAction= KeyAction.UP;
        foreach(var p in settingData.EachKey())
        {
            if(p.Item1== keyCode)
            {
                overlapAction = p.Item2;
                isKeyInSetting = true;
                break;

            }
        }

        if (isKeyInSetting)
        {
            overlapText.GetComponent<Text>().text = $"이 키는 \"{overlapAction.ToString()}\"와 중복됩니다.";
            ShowOverlapText();
            yield break;
        }

        switch (action)
        {
            case KeyAction.UP:
                settingData.UpKeySettingValue = keyCode;
                break;
            case KeyAction.DOWN:
                settingData.DownKeySettingValue = keyCode;
                break;
            case KeyAction.LEFT:
                settingData.LeftKeySettingValue = keyCode;
                break;
            case KeyAction.RIGHT:
                settingData.RightKeySettingValue = keyCode;
                break;
            case KeyAction.INTERACTION:
                settingData.InteractionKeySettingValue = keyCode;
                break;
        }
        UpdateKeyUI();
    }
    public float visableTime = 3f;
    float disappearTime = 1f;
    float currentTime = 0;
    IEnumerator currentCoroutine;

    public void ShowOverlapText()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentTime = 0;
        currentCoroutine = OverlapTextIE();
        StartCoroutine(currentCoroutine);
    }

    private IEnumerator OverlapTextIE()
    {
        Color overlapTextAlpha = overlapText.GetComponent<Text>().color;
        overlapTextAlpha.a = 1f;
        overlapText.GetComponent<Text>().color = overlapTextAlpha;
        yield return new WaitForSecondsRealtime(visableTime);
        while (currentTime <= disappearTime)
        {
            currentTime += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, currentTime / disappearTime);
            overlapTextAlpha.a = 1-t;
            overlapText.GetComponent<Text>().color = overlapTextAlpha;
            yield return null;
        }
        currentCoroutine = null;
    }

    public void ChangeKey(int num) //OnClick에 연결할 메서드 생성
    //int 매개변수를 추가하고 key를 초기화.
    {
        KeyAction keyAction = (KeyAction)num;
        StartCoroutine(ChageKeyIE(keyAction));
    }
}
