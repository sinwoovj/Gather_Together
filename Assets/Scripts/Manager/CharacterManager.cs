using Cinemachine;
using DI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class CharacterManager : DIMono
{
    [Inject]
    PlayData playData;

    [Inject]
    GameData gameData;

    public GameObject VCam;
    public GameObject Member;
    public void Update()
    {
        int res;
        Int32.TryParse(Input.inputString, out res);
        if (res >= 1 && res < gameData.Member.Count)
        {
            CharacterChange(res);
        }
    }
    public void CharacterChange(int CharacterNum)
    {
        if (CharacterNum == playData.presentChar)
        {
            return;
        }

        Debug.Log($"{playData.presentChar} : {CharacterNum} : {gameData.Member.Single(l => l.Id == CharacterNum).Name}");
        // ������ ĳ���� Transform
        Transform PresentChar = Member.transform.GetChild(CharacterNum);
        VCam.GetComponent<CinemachineVirtualCamera>().Follow = PresentChar.transform;        // ī�޶� ��ȯ
        PresentChar.GetComponent<PlayerAction>().enabled = true;        // ��Ʈ�� Ȱ��ȭ
        PresentChar.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;         // Rigidbody Static -> Dynamic
        PresentChar.gameObject.layer = 7;        // Object Layer 'NPC & Object'���� 'Player'�� ����
        PresentChar.GetComponent<Animator>().enabled = true;       // Animator �ѱ�

        // ���� ĳ���� Transform
        Transform PrevChar = Member.transform.GetChild(playData.presentChar);
        PrevChar.GetComponent<PlayerAction>().enabled = false;        // ���� ĳ���� ��Ʈ�� ��Ȱ��ȭ
        PrevChar.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;         // Rigidbody Static -> Dynamic
        PrevChar.gameObject.layer = 6;        // Object Layer 'NPC & Object'���� 'Player'�� ����
        PrevChar.GetComponent<Animator>().enabled = false;       // Animator ����

        // ���� ĳ���� ���� ��ȣ�� �°� ��ȯ
        playData.presentChar = CharacterNum;
    }
}
