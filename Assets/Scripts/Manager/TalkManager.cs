using DI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TalkManager : DIMono
{
    [Inject]
    QuestManager questManager;

    [Inject]
    ChatManager chatManager;

    [Inject]
    SelectManager selectManager;

    [Inject]
    PlayData playData;

    [Inject]
    GameData gameData;

    [Inject]
    SceneChanger sceneChanger;

    public List<SceneLine> sceneLines;

    public IEnumerator StartScene(int sceneLineCode)
    {
        yield return null;

        playData.isAction = true;
        IEnumerable<SceneLine> sceneLines = gameData.SceneLine.Where(l => l.code == sceneLineCode);

       

        foreach (var line in sceneLines)
        {
            if (playData.NeedSkip && line.LineType!= lineType.Chat)
            {
                playData.NeedSkip = false;
            }

            switch (line.LineType)
            {
                case lineType.Chat:
                    chatManager.Chat(sceneLines, line);
                    while (playData.isTypeEffectAnim)
                    {
                        yield return null;
                    }
                    while (Input.GetKeyUp(KeyCode.F) == false)
                    {
                        if (playData.NeedSkip)
                        {
                            yield return new WaitForSeconds(0.1f);
                            break;
                        }
                        yield return null;
                    }
                    yield return null;
                    break;
                case lineType.CloseChat:
                    chatManager.CloseChat();
                    break;
                case lineType.Selection:
                    selectManager.SelectSetActive(true);
                    selectManager.Select(line.intValues);
                    while (true)
                    {
                        if (playData.selectNumber != -1)
                        {
                            selectManager.SelectResultProcess(line.intValues,playData.selectNumber);
                            playData.selectNumber = -1;
                            break;
                        }
                        yield return null;
                    }
                    selectManager.SelectSetActive(false);
                    break;
                case lineType.NextEvent:

                    break;
                case lineType.Wait:
                    yield return new WaitForSeconds(float.Parse(line.content));
                    break;
                case lineType.LoadScene:
                    sceneChanger.ChangeScene(line.content, SceneChanger.LoadingScene.FadeInOut);
                    break;
                case lineType.SetSubQuest:
                    questManager.SetSubQuest(Int32.Parse(line.content));
                    break;
                case lineType.SetMainQuest:
                    questManager.SetMainQuest(Int32.Parse(line.content));
                    break;
            }
        }
        playData.isAction = false;
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
