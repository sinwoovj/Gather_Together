using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
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



public class TalkManager : MonoBehaviour
{
    public GameManager gameManager;
    public QuestManager questManager;


    //대화 테이터를 저장할 Dictionary 변수 생성
    Dictionary<long, string> nameData; // 이름 데이터를 저장할 Dictionary 변수 생성
    public Dictionary<long, string[]> talkData;
    Dictionary<long, Sprite> portraitData; // 초상화 데이터를 저장할 Dictionary 변수 생성

    public Sprite[] portaitArr;
    public int LudoTalkCount = 0;

    void Awake()
    {
        nameData = new Dictionary<long, string>();
        talkData = new Dictionary<long, string[]>();
        portraitData = new Dictionary<long, Sprite>();      
        GenerateData();
    }

    void GenerateData()
    {
    /*
    code 
    종류  - 고정오브젝트,Character, Item
    메인퀘스트 
    서브퀘스트
    대화 인덱스
    */

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

    /*
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
    */

    /*
    이벤트 코드
    대사 하는 주체
    주체의 상태 - 나오는 포트레이트의 감정 등
    인덱스
    대사 내용
    */
    }
}
