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
    //���� ���� ��ư Ȱ��ȭ ��Ű�� content�� SceneLinecode �ѱ��
    //���� ��ư�� SceneLineCode�� �������� ����
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
