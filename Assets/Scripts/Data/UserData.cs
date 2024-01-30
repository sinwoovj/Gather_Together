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
    public string campsiteName = "슈룹"; // 캠핑장 이름
    public Vector2 playerLoc; // 플레이어 위치
    public int mainQuestId = 0; // 메인 퀘스트
    public int subQuestId = 0; // 서브 퀘스트
    public int money = 0; // 돈
    public int stamina; // 스태미나
    public float processivity = 0; // 진행도
    public string cityName = "Default"; // 지역
    public DateTime startedTime = DateTime.Now; // 시작한 날짜
    public List<InvenSlot> inventory = new List<InvenSlot>();
 
    internal void CopyFrom(UserData userData)
    {
        campsiteName = userData.campsiteName;
        playerLoc = userData.playerLoc;
        mainQuestId = userData.mainQuestId;
        subQuestId = userData.subQuestId;
        money = userData.money;
        stamina = userData.stamina;
        processivity = userData.processivity;
        cityName = userData.cityName;
        startedTime = userData.startedTime;
        inventory= userData.inventory;

    }
    internal void InitialValue(UserData userData)
    {
        userData.campsiteName = "";
        userData.mainQuestId = 0;
        userData.subQuestId = 0;
        userData.money = 0;
        userData.stamina = 0;
        userData.processivity = 0;

    }

    internal void FillUserPosition(Transform transform)
    {
        playerLoc = transform.position;
    }

    public DateTime GetPresentTime(string cityName)
    {
        TimeZoneInfo timeZone = null;

        // 도시 이름에 따라 시간대 설정
        switch (cityName)
        {
            case "Toronto":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                break;
            case "LosAngeles":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                break;
            case "Washington":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                break;
            case "RioDeJaneiro":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                break;
            case "BuenosAires":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
                break;
            case "London":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                break;
            case "Rome":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                break;
            case "Cairo":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                break;
            case "CapeTown":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");
                break;
            case "Kuwait":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time");
                break;
            case "Moscow":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
                break;
            case "Beijing":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
                break;
            case "Tokyo":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
                break;
            case "Sydney":
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
                break;
            default:
                // 기본 시간대 (예: 서울)
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time");
                break;
        }

        if (timeZone != null)
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);
        }
        else
        {
            // 시간대를 찾을 수 없는 경우 기본 시간대 사용
            return DateTime.Now;
        }
    }
}
