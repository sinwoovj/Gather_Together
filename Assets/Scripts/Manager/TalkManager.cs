using DI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

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
    QuestManager questManager;

    [Inject]
    PlayData playData;

    [Inject]
    GameData gameData;

    public GameObject TextPanel;
    public Texture2D Portait;
    public string Portait_Address;

  
    private void Handle_Completed(AsyncOperationHandle<Texture2D> operation)
    {
        if(operation.Status == AsyncOperationStatus.Succeeded)
        {
            Portait = operation.Result;
        }
        else Debug.LogError($"Asset For {Portait_Address} failed to load.");
    }

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

                    // 채팅 화면에보여주기
                    TextPanel.GetComponent<Animator>().SetBool("isShow", true);
                    // 초상화 설정
                    Transform TalkPanel_Portrait = TextPanel.transform.Find("Portrait");


                    Portait_Address = gameData.HostImage.Single(
                       l => l.hostCode == sceneLines.FirstOrDefault().hostCode 
                    && l.clothCode == sceneLines.FirstOrDefault().clothCode 
                    && l.hostEmotion == sceneLines.FirstOrDefault().hostEmotion).assetPath;
                    Debug.Log(Portait_Address);
               
                    var porait= Addressables.LoadAssetAsync<Texture2D>(Portait_Address).WaitForCompletion();
                    var rawImage = TalkPanel_Portrait.GetComponent<RawImage>();
                    

                    rawImage.texture = porait;
                    // 이름 설정
                    Transform TalkPanel_Name = TextPanel.transform.Find("Name");
                    Member interlocutor = gameData.Member.FirstOrDefault(l => l.Id == line.hostCode);
          
                    if (interlocutor != null)
                    {
                        TalkPanel_Name.GetComponent<Text>().text = interlocutor.Name;
                    }
                    // 대화 설정
                    Transform TalkPanel_Script = TextPanel.transform.Find("Script");
                

                    TalkPanel_Script.GetComponent<Text>().text = line.content;
                    Debug.Log(" line.content " + line.content + " isFKeyPressed]" );

                    while (Input.GetKeyUp(KeyCode.F) == false )
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
                    TextPanel.GetComponent<Animator>().SetBool("isShow", false);
                    break;
                case lineType.Selection:

                    break;
                case lineType.NextEvent:

                    break;
                case lineType.Wait:

                    break;
                case lineType.LoadScene:
                    SceneManager.LoadScene(line.content);
                    break;
                case lineType.NextSubQuest:
                    questManager.NextSubQuest();
                    break;
                case lineType.NextMainQuest:
                    questManager.NextMainQuest();
                    break;
                
            }


        }


        playData.isAction = false;
        Debug.Log("SceneDone " + sceneLineCode);
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
