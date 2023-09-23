using DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SelectManager : DIMono
{
    [Inject]
    GameData gameData;

    [Inject]
    TalkManager talkManager;

    public GameObject SelectGameObject;

    //갯수 토대로 버튼 활성화 시키고 content와 SceneLinecode 넘기기
    //누른 버튼의 SceneLineCode를 바탕으로 진행
    public void Select(IntArr intValues)
    {
        Transform SelectButtonLayOut = SelectGameObject.transform.GetChild(1);
        // Selection 갯수에 따른 버튼 갯수 띄우는 부분
        for (int i = 0; i < intValues.SelectCount(); i++)
        {
            Transform BeActiveObject = SelectButtonLayOut.transform.GetChild(i);
            BeActiveObject.transform.GetChild(0).GetComponent<Text>().text = 
            gameData.SceneSelection.FirstOrDefault(l => l.code == intValues.values[i]).content;
            BeActiveObject.gameObject.SetActive(true);
        }
    }
    
    public void SelectResultProcess(IntArr intValues, int selectNumber)
    {
        Debug.Log(intValues.values[selectNumber]);
        StartCoroutine(talkManager.StartScene(gameData.SceneSelection.FirstOrDefault(l => l.code == intValues.values[selectNumber]).sceneLineCode));
    }

    public void SelectSetActive(bool active)
    {
        if(active == false)
        {
            Transform SelectButtonLayOut = SelectGameObject.transform.GetChild(1);
            for (int i = 0; i < 5; i++)
            {
                Transform BeActiveObject = SelectButtonLayOut.transform.GetChild(i);
                BeActiveObject.gameObject.SetActive(false);
            }
        }
        SelectGameObject.SetActive(active);
    }
}
