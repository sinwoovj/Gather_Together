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

    // �ε����� �����մϴ�.
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

    // IEnumerable<int>�� GetEnumerator() �޼��带 �����մϴ�.
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


    public int hostCode; // 1. ���ΰ� 2~6. ��� ... ��Ÿ
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
    { //� �ൿ�� �Ұ��ΰ�?
        /*
        ToUnityScne ����Ƽ�� ���� �̵� (ActionValStr)
        ToStartScne ���Ӿ��� ���� SceneLine���� ǥ��� ��
        LikeabilityCondition ȣ������ ���� �ٲ�
        StatCondition ���� ���ȿ� ���� �ٲ�
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