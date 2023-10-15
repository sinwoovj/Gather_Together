using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class InvenSlot
{
    public int index;
    public int itemCode;
    public int count;
}

[Serializable]
public class UserData
{
    public Vector2 PlayerLoc; // 플레이어 위치
    public int MainQuestId;
    public int SubQuestId;
    public int gold;
    public int stmina;

    public List<InvenSlot> Inventory = new List<InvenSlot>();

    internal void CopyFrom(UserData userData)
    {
        PlayerLoc = userData.PlayerLoc;
        MainQuestId = userData.MainQuestId;
        SubQuestId = userData.SubQuestId;
        gold = userData.gold;
        stmina = userData.stmina;

    }
    internal void InitialValue(UserData userData)
    {
        userData.MainQuestId = 0;
        userData.SubQuestId = 0;
        userData.gold = 0;
        userData.stmina = 0;
    }

    internal void FillUserPostion(Transform transform)
    {
        PlayerLoc = transform.position;
    }
}
