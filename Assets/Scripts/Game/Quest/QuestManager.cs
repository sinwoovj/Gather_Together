using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int QuestId;
    public int SubQuestId;
    public int QuestActionIndex; //퀘스트 대화 순서 변수 생성
    public GameObject[] questObject; //퀘스트 오브젝트를 저장할 변수 생성

    Dictionary<long, MainQuestData> mainQuestList;
    Dictionary<long, SubQuestData> subQuestList;

    void Awake()
    {
        mainQuestList = new Dictionary<long, MainQuestData>();
        subQuestList = new Dictionary<long, SubQuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        //Add 함수로 <QuestId, QuestData> 데이터를 저장
        mainQuestList.Add(0, new MainQuestData("Quest 0. 게임 시작."
                                        , new long[] { 701000000000, 700000000000 }));
        mainQuestList.Add(1000, new MainQuestData("Quest 1. 마을 사람들과 대화하기."
                                        , new long[] { 701000000000, 700000000000 }));
        mainQuestList.Add(2000, new MainQuestData("Quest 2. 루도의 동전 찾아주기."
                                        , new long[] { 356000000000, 700000000000 }));
        mainQuestList.Add(3000, new MainQuestData("Quest 3. 퀘스트 올 클리어!"
                                        , new long[] { 0 }));

        //Add 함수로 <SubQuestId, QuestData> 데이터를 저장
        subQuestList.Add(0, new SubQuestData("SubQuest 0. 서브퀘스트 시작!", new long[] { 701000000000, 700000000000 }));
        //Random.Range 메서드로 subQuestList(리스트)에 여러 서브 퀘스트들을 랜덤 순서로 넣음.
        Dictionary<int, int> randomValueForSubQuest;
        randomValueForSubQuest = new Dictionary<int, int>();
        int randomValue;
        for (int i = 1; i < 4;)
        {
            randomValue = Random.Range(1, 4); // Random.Range(min, max - 1)
            if(!randomValueForSubQuest.ContainsValue(randomValue))
            {
                randomValueForSubQuest.Add(i, randomValue);
                switch (randomValue)
                {
                    case 1:
                        Debug.Log("SubQuest" + i + ". 꼬마 손님 10명 받기.");
                        subQuestList.Add(i * 1000000, new SubQuestData("SubQuest" +
                            i + ". 꼬마 손님 10명 받기.", new long[] { 1000000000, 2000000000 }));
                        break;
                    case 2:
                        Debug.Log("SubQuest" + i + ". 분리수거 10번 하기.");
                        subQuestList.Add(i * 1000000, new SubQuestData("SubQuest" +
                            i + ". 분리수거 10번 하기.", new long[] { 1000000000, 2000000000 }));
                        break;
                    case 3:
                        Debug.Log("SubQuest" + i + ". 낚시터 청소 10번 하기.");
                        subQuestList.Add(i * 1000000, new SubQuestData("SubQuest" +
                            i + ". 낚시터 청소 10번 하기.", new long[] { 1000000000, 2000000000 }));
                        break;
                }
                i++;
            }
        }
        subQuestList.Add(4000000, new SubQuestData("SubQuest 4. 서브퀘스트 올클!", new long[] { 0 }));
    }

    //NPC Id를 받고 퀘스트 번호를 반환하는 함수 생성
    public int GetQuestTalkIndex(long id)
    {
        //퀘스트번호 + 퀘스트 대화순서 = 퀘스트 대화 Id
        return SubQuestId + QuestId + QuestActionIndex;
    }
        
    //대화 진행을 위해 퀘스트 대화순서를 올리는 함수 생성
    public string CheckQuest(long id)
    {
        //Next Talk Target
        Debug.Log("QuestId : " + QuestId + " / QuestActionIndex : " + QuestActionIndex);
        if (id == mainQuestList[QuestId].npcId[QuestActionIndex]) // 즉, 1000 = mainQuestList[10].npcId[0]
            QuestActionIndex++;
        //Control Quest Object
        ControlObject();
        //Talk Complete & Next Quest
        if (QuestActionIndex == mainQuestList[QuestId].npcId.Length)
            NextQuest();
        //Quest Name
        return mainQuestList[QuestId].MainQuestName;
    }

    public string CheckSubQuest(long id)
    {
        //Next Talk Target
        Debug.Log("SubQuestId : " + SubQuestId + " / QuestActionIndex : " + QuestActionIndex);
        if (id == subQuestList[SubQuestId].npcId[QuestActionIndex])
            QuestActionIndex++;
        //Control Quest Object
        SubControlObject();
        //Talk Complete & Next Quest
        if (QuestActionIndex == subQuestList[SubQuestId].npcId.Length)
            NextSubQuest();
        //Quest Name
        return subQuestList[SubQuestId].SubQuestName;
    }

    //위의 함수와 함수명이 같지만 매개함수가 다르면 구분이 가능함.
    //이 기술은 다음과 같다. >> 오버로딩(Overloading) : 매개변수에 따라 함수 호출
    public string CheckQuest()
    {
        //Quest Name
        return mainQuestList[QuestId].MainQuestName;
    }
    public string CheckSubQuest()
    {
        //Quest Name
        return subQuestList[SubQuestId].SubQuestName;
    }
    public void NextQuest()
    {
        QuestId += 1000;
        QuestActionIndex = 0;
    }
    public void NextSubQuest()
    {
        SubQuestId += 1000000;
        QuestActionIndex = 0;
    }
    public void ControlObject() //퀘스트 오브젝트를 관리할 함수 생성
    {
        switch (QuestId){ //퀘스트번호, 퀘스트 대화순서에 따라 오브젝트 조절
            case 1000:
                if (QuestActionIndex == 2)
                    questObject[0].SetActive(true);
                break;
            case 2000:
                //불러오기 했을 당시의 퀘스트 순서와 연결된 오브젝트 관리 추가
                if (QuestActionIndex == 0)
                    questObject[0].SetActive(true);
                else if (QuestActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
        }
    }
    public void SubControlObject() //퀘스트 오브젝트를 관리할 함수 생성
    {
        switch (SubQuestId)
        { //퀘스트번호, 퀘스트 대화순서에 따라 오브젝트 조절
            case 0:
                break;
            case 1000000:
                break;
            case 2000000:
                break;
            case 3000000:
                break;
        }
    }
}