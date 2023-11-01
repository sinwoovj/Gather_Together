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

        // ���� �̸��� ���� �ð��� ����
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
                // �⺻ �ð��� (��: ����)
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time");
                break;
        }

        if (timeZone != null)
        {
            userData.currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);
        }
        else
        {
            // �ð��븦 ã�� �� ���� ��� �⺻ �ð��� ���
            userData.currentTime = DateTime.Now;
        }
    }
}
