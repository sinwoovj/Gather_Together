using DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    [Inject]
    GameData gameData;
    [Inject]
    TalkManager talkManager;

    public GameObject Selection;

    int selectNumber = 0;
    //갯수 토대로 버튼 활성화 시키고 content와 SceneLinecode 넘기기
    //누른 버튼의 SceneLineCode를 바탕으로 진행
    public void Select(IntArr intValues)
    {
        int count = intValues.ToL
        StartCoroutine(talkManager.StartScene());
        
    }
    public void SelectSetActive(bool active) 
    {
        Selection.SetActive(active);
    }
}
