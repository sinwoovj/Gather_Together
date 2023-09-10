using System.Collections.Generic;
using System.Linq;

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

public enum LineType
{
    Chat,
    Selection,
    NextEvent,
}
[System.Serializable]
public class SceneLine
{
    public int code;

    public int index;

    public LineType LineType;


    public int hostCode;
    public int clothCode;
    public int hostEmotion;


    public string content;
    public IntArr intValues;
}