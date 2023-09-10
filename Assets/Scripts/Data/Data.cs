using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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


    public enum SEX
    {
        M,
        F,
        D
    }
}
[System.Serializable]
public class MainQuest
{
    public int Id;
    public string Name;
    public string Condition;
    public string Content;
}

[System.Serializable]
public class SubQuest
{
    public int Id;
    public string Name;
    public string Condition;
    public string Content;
}




