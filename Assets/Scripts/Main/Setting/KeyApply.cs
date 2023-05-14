using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyApply : MonoBehaviour
{
    KeyManager keyManager;

    public void Awake()
    {
        keyManager = GameObject.Find("Key").GetComponent<KeyManager>();
    }
    public void KeyApplyF()
    {
        for (int i = 0; i < (int)StandByKeyAction.KEYCOUNT; i++)
        {
            StandByKeySettingValue.StandBykeys[(KeyAction)keyManager.key] = keyManager.defaultKeys[i];
            //for문을 통해서 defaultKeys에 저장된 배열을 순서대로 StandByKeyAction에 값 추가
        }
    }
}
