using DI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestClearChecker 
{
    [Inject]
    PlayerAction playerAction;

    [Inject]
    UserData userData;


    [Inject]
    GameData gameData;

    public bool IsQuestCleared(int QuestCondtionCode)
    {
        foreach(var q  in gameData.QuestCondition.Where(l=>l.code== QuestCondtionCode))
        {

            var successed =IsConditionSuccess(q);
            if (successed == false)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsConditionSuccess(QuestCondition q)
    {

        switch (q.Type)
        {
            case QuestCondition.QuestConditionType.XYPosition:
                return CheckXYPosition(q.val1, q.val2);
            case QuestCondition.QuestConditionType.GoldHave:
                return HaveGold((int)q.val1);
        }

        return true;

    }

    public bool CheckXYPosition(float x, float y) {

        const float CloseEnoughDist = 10;
        var v = playerAction.transform.position - new Vector3(x, y);
        return v.magnitude< CloseEnoughDist;
    } 

    public bool HaveGold(int gold) {

        return userData.gold >= gold;


    }
}
