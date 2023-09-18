using DI;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class NameData
{
    public int code;

    public enum NameType
    {
        FixObject,
        Character,
        Item,
    }

    public NameType type;
    public string name;

}



public class TalkManager : DIMono
{
    [Inject]
    GameManager gameManager;

    [Inject]
    PlayData playData;

    [Inject]
    GameData gameData;

    public QuestManager questManager;

    bool isFKeyPressed = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFKeyPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            isFKeyPressed = false;
        }
    }

    public IEnumerator StartScene(int sceneLineCode)
    {
        playData.actionBlock = true;
        var sceneLines = gameData.SceneLine.Where(l => l.code == sceneLineCode).OrderBy(l => l.index);


        foreach (var line in sceneLines)
        {
            switch (line.LineType)
            {
                case lineType.Chat:

                    //채팅 화면에보여주기


                    while (true)
                    {
                        yield return null;
                        if (isFKeyPressed)
                        {
                            isFKeyPressed = false;
                            break;
                        }
                    }

                    break;
                case lineType.Selection:

                    break;
                case lineType.NextEvent:

                    break;
                case lineType.SetSubQuest:

                    break;
                case lineType.SetMainQuest:

                    break;
            }


        }
        playData.actionBlock = false;
        UnityEngine.Debug.Log("SceneDone " + sceneLineCode);
    }

    /*
    code 
    종류  - 고정오브젝트,Character, Item
    메인퀘스트 
    서브퀘스트
    대화 인덱스

    // 현재 상태 
    // 메인퀘스트 상태

    // 메인퀘스트 상태로 다음 메인퀘스트를 알 수 있다.
    // 서브퀘스트는 현재의 상태로 체크해서 시작할 수 있는 여부를 체크할 수 있다.
    // 메인퀘스트 시작조건
    //   열리는조건 이전메인퀘스트가 끝났을 것.

    //게임의 이벤트 
    //  코드
    //  이름 
    //  List<조건> 
    //  연출 코드

    //서브퀘스트 
    // 열리는 조건 - 메인퀘스트 1번이 완료되어야한다/
    //   서브퀘스트 X가 몇번이상 수행되어야한다.
    //   날짜가 몇일 되어야한다.
    //   아이템이 뭐가있어야한다.

    // 메인퀘스트 1 번 이벤트이다.
    // 보상은 뭐다
    // 이다음은 2번 열린다.

    1번이벤트
    이벤트 시작 |
    대사 | 1 번이벤트에 A 라는 사람이  "안녕하세요"   1번이미지로 나타냄
    대사 | 1 번이벤트에 B 라는 사람이  "안녕하세요"   1번이미지로 나타냄
    배경일러 변경 |
    선택지
        1번 선택지 - 어쩌고저쩌고
            등장조건 -
            연결되는 이벤트 코드
        2번 선택지 - 어쩌고 저쩌고
            등장조건 -
            연결되는 이벤트 코드
    이벤트 끝 |       


    이벤트 코드
    대사 하는 주체
    주체의 상태 - 나오는 포트레이트의 감정 등
    인덱스
    대사 내용
    */
}
