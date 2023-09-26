public interface IFillFromStr
{
    void FillFromString(string str);
}

public class Pair : IFillFromStr
{
    public int v1, v2;

    public void FillFromString(string str)
    {
        var arr = str.Split(',');


        v1 = int.Parse(arr[0]);
        v2 = int.Parse(arr[1]);
    }
}

public class Range : IFillFromStr
{
    public int min, max;
    public void FillFromString(string str)
    {
        var arr = str.Split(',');

        min = int.Parse(arr[0]);
        max = int.Parse(arr[1]);
    }
}


[System.Serializable]
public class NPC
{
    public int Id;
    public string Type;
    public SEX Sex;
    public string Name;
    public string Age;

    public enum SEX
    {
        None,
        M,
        F,
        D
    }
}

[System.Serializable]
public class Member
{
    public int Id;
    public SEX Sex;
    public string Name;
    public int Age;
    public string Birth;
    public int Height;

    public enum SEX
    {
        None,
        M,
        F
    }
}


[System.Serializable]
public class QuestCondition
{
    public int code;
    public QuestConditionType Type;

    public enum QuestConditionType
    {
        XYPosition,
        GoldHave
    }

    public float val1;
    public float val2;
}