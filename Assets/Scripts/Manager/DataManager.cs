using DI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : DIMono
{
    [Inject]
    UserData userData;

    [Inject]
    GameObject player;

    public void GameSave() //저장 함수
    {
        //PlayerPrefs : 간단한 데이터 저장 기능을 지원하는 클래스 // 데이터 타입에 맞게 Set 함수 사용 (SetInt/float/string)
        userData.FillUserPosition(player.transform);
        var json = JsonUtility.ToJson(userData);


        PlayerPrefs.SetString("UserDataJSON", json);
        //저장
        PlayerPrefs.Save();
    }

    public void GameLoad() //불러오기 함수
    {
        //최초 게임 실행했을 땐 데이터가 없으므로 예외처리 로직 작성
        if (!PlayerPrefs.HasKey("UserDataJSON"))
        {
            this.userData.InitialValue(userData);
            return;
        }
        var json = PlayerPrefs.GetString("UserDataJSON");
        var userData1 = JsonUtility.FromJson<UserData>(json);
        userData.CopyFrom(userData1);

        //세팅 : 불러온 데이터를 게임 오브젝트에 적용
        player.transform.position = userData1.playerLoc;
    }
}
