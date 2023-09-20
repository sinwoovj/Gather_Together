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

    public void GameSave() //���� �Լ�
    {
        //PlayerPrefs : ������ ������ ���� ����� �����ϴ� Ŭ���� // ������ Ÿ�Կ� �°� Set �Լ� ��� (SetInt/float/string)
        userData.FillUserPostion(player.transform);
        var json = JsonUtility.ToJson(userData);


        PlayerPrefs.SetString("UserDataJSON", json);
        //����
        PlayerPrefs.Save();
    }

    public void GameLoad() //�ҷ����� �Լ�
    {
        //���� ���� �������� �� �����Ͱ� �����Ƿ� ����ó�� ���� �ۼ�
        if (!PlayerPrefs.HasKey("UserDataJSON"))
        {
            this.userData.InitialValue(userData);
            return;
        }
        var json = PlayerPrefs.GetString("UserDataJSON");
        var userData1 = JsonUtility.FromJson<UserData>(json);
        userData.CopyFrom(userData1);

        //���� : �ҷ��� �����͸� ���� ������Ʈ�� ����
        player.transform.position = userData1.PlayerLoc;
    }
}
