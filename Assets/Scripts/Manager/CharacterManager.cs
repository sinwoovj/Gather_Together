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
        // 선택한 캐릭터 Transform
        Transform PresentChar = Member.transform.GetChild(CharacterNum);
        VCam.GetComponent<CinemachineVirtualCamera>().Follow = PresentChar.transform;        // 카메라 전환
        PresentChar.GetComponent<PlayerAction>().enabled = true;        // 컨트롤 활성화
        PresentChar.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;         // Rigidbody Static -> Dynamic
        PresentChar.gameObject.layer = 7;        // Object Layer 'NPC & Object'에서 'Player'로 변경
        PresentChar.GetComponent<Animator>().enabled = true;       // Animator 켜기

        // 이전 캐릭터 Transform
        Transform PrevChar = Member.transform.GetChild(playData.presentChar);
        PrevChar.GetComponent<PlayerAction>().enabled = false;        // 이전 캐릭터 컨트롤 비활성화
        PrevChar.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;         // Rigidbody Static -> Dynamic
        PrevChar.gameObject.layer = 6;        // Object Layer 'NPC & Object'에서 'Player'로 변경
        PrevChar.GetComponent<Animator>().enabled = false;       // Animator 끄기

        // 현재 캐릭터 상태 번호에 맞게 변환
        playData.presentChar = CharacterNum;
    }
}
