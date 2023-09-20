using Mono.Cecil.Cil;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;

[System.Serializable]
public class GameScene
{
    public int code;
    public string name;
}

[System.Serializable]
public class HostImage
{
    public int hostCode;
    public int clothCode;
    public int emotion;
    public string assetPath;
}


[System.Serializable]
public class SceneSelection
{
    public int code;
    public string content;
    public string conditionCodes;
}

[System.Serializable]
public class SceneCondition
{
    public int code;


    public enum ConditionKind
    {
        ItemHave,
        StatUpper,
        StatBelow,
        EventDone
    }
    public ConditionKind kind;


    public int value;
    public int count;
}

[System.Serializable]
public class IntArr : IEnumerable<int>, IFillFromStr
{
    public List<int> values;

    // 인덱서를 정의합니다.
    public int this[int index]
    {
        get { return values[index]; }
        set { values[index] = value; }
    }

    public void FillFromString(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            values = new List<int>();
            return;
        }

        values = str.Split(';').Select(l => int.Parse(l)).ToList();
    }

    // IEnumerable<int>의 GetEnumerator() 메서드를 구현합니다.
    public IEnumerator<int> GetEnumerator()
    {
        foreach (var value in values)
        {
            yield return value;
        }
    }



    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
public enum lineType
{
    Chat,
    SetMainQuest,
    SetSubQuest,
    Selection,
    NextEvent,
}

[System.Serializable]
public class SceneLine
{
    public int code;

    public int index;

    public lineType LineType;


    public int hostCode; // 1. 주인공 2~6. 멤버 ... 기타
    public int clothCode;
    public int hostEmotion;


    public IntArr intValues;
    public string content;
}
[System.Serializable]
public class ScenarioData
{
    public int MainQuestId;
    public int SubQuestId;
    public enum hostType
    {
        None,
        NPC,
        Member,
        Item,
        Object,
        Tower,
    }
    public hostType HostType;
    public int Id;

    public enum scenarioActionType
    { //어떤 행동을 할것인가?
        /*
        ToUnityScne 유니티의 씬을 이동 (ActionValStr)
        ToStartScne 게임씬을 시작 SceneLine으로 표기된 씬
        LikeabilityCondition 호감도에 따라 바뀜
        StatCondition 현재 스탯에 따라 바뀜
         */
        Talk,
        ToUnityScene,
        ToStartScene,
        LikeabilityCondition,
        StatCondition,
    }
    public scenarioActionType ScenarioActionType;
    public string ActionValStr;
    public int IntVal;
}

[System.Serializable]
public class LikeabilityCondition
{
    public int code;
    public string condition;   
    public int targetNPC;
    public int likeability;
    public int SceneLine;
}
[System.Serializable]
public class StatCondition
{
    public int code;
    public string condition;
    public StatType statType;
    public enum StatType
    {
        HP,
        Stamina
    }
    public int Value;
    public int SceneLine;
}