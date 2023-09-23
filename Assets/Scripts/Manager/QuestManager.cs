using DI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : DIMono
{
    [Inject]
    GameData gameData;

    [Inject]
    PlayData playData;

    [Inject]
    UserData userData;

    public GameObject[] questObject; //퀘스트 오브젝트를 저장할 변수 생성
    public Text mainQuestText;
    public Text subQuestText;

    // userData 안에 있는 현재 QuestId로 퀘스트 이름과 내용을 알아낸다.
    public MainQuest CheckMainQuestData()
    {
        MainQuest temp = gameData.MainQuest.Single(l => l.MainQuestId == userData.MainQuestId) as MainQuest;
        if (temp == null)
        {
            Debug.LogError($"{userData.MainQuestId}에 해당하는 메인 퀘스트 데이터가 없다.");
            return null;
        }
        return temp;
    }
    public SubQuest CheckSubQuestData()
    {
        SubQuest temp = gameData.SubQuest.Single(l => l.SubQuestId == userData.SubQuestId) as SubQuest;
        if (temp == null)
        {
            Debug.LogError($"{userData.SubQuestId}에 해당하는 서브 퀘스트 데이터가 없다.");
            return null;
        }
        return temp;
    }
    public void SetQuestText()
    {
        mainQuestText.text = CheckMainQuestData() == null ? "" : CheckMainQuestData().MainQuestName;
        subQuestText.text = CheckSubQuestData() == null ? "" : CheckSubQuestData().SubQuestName;
    }
    public void SetMainQuest(int id)
    {
        userData.MainQuestId = id;
        playData.questDetective = true;
    }
    public void SetSubQuest(int id)
    {
        userData.SubQuestId = id;
        playData.questDetective = true;
    }
}