using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text에 접근하기 위해

public class UIManager : MonoBehaviour
{
    public Text[] txt; //Text 변수를 배열로 선언
    void Start()
    {
        for(int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySettingValue.keys[(StandByKeyAction)i].ToString();
        }
        //for문을 사용하여 Text의 내용을 keys의 값에 차례로 넣음 
    }

    void Update()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySettingValue.keys[(StandByKeyAction)i].ToString();
        }
    }
}
