using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MainQuest
{
    public int MainQuestId;

    public string MainQuestName; //quest 이름

    public int MainQuestCondition;

    public string MainQuestContent;

    public void MainQuest_(int mainQuestId, string mainQuestName, int mainQuestCondition, string mainQuestContent)
    {
        MainQuestId = mainQuestId;
        MainQuestName = mainQuestName;
        MainQuestCondition = mainQuestCondition;
        MainQuestContent = mainQuestContent;
    }

    //구조체 생성을 위해 매개변수 생성자를 작성
}
[System.Serializable]
public class SubQuest
{
    public int SubQuestId;

    public string SubQuestName; //quest 이름

    public int SubQuestCondition;

    public string SubQuestContent;

    //구조체 생성을 위해 매개변수 생성자를 작성
    public void SubQuest_(int subQuestId, string subQuestName, int subQuestCondition, string subQuestContent)
    {
        SubQuestId = subQuestId;
        SubQuestName = subQuestName;
        SubQuestCondition = subQuestCondition;
        SubQuestContent = subQuestContent;
    }

}

