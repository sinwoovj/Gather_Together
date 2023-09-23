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

    //���� ���� ��ư Ȱ��ȭ ��Ű�� content�� SceneLinecode �ѱ��
    //���� ��ư�� SceneLineCode�� �������� ����
    public void Select(IntArr intValues)
    {
        Transform SelectButtonLayOut = SelectGameObject.transform.GetChild(1);
        // Selection ������ ���� ��ư ���� ���� �κ�
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
