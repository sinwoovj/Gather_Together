using System;
using DI;

public class Localize : DIMono
{
    [Inject]
    UserData userData;

    public void SetTime(string cityName)
    {
        userData.cityName = cityName;
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
            userData.currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);
        }
        else
        {
            // 시간대를 찾을 수 없는 경우 기본 시간대 사용
            userData.currentTime = DateTime.Now;
        }
    }
}
