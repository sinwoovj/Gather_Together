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
    [Header("게임메니저")]
    public GameManager gameManager;
    public QuestManager questManager;

    [Range(0,100)]
    public float range;

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
        id 값 [ 고정 오브젝트(000 ~ 350), 아이템(356~699) , 캐릭터 및 NPC(700~999) + 000000000]

        Ludo :700
        Luna :701
        나무상자 :001
        나무 책상 :002
        */
        //  << Name Data >>

        //NPC
        nameData.Add(700000000000, "루도");
        nameData.Add(701000000000, "루나");

        //Object
        nameData.Add(1000000000, "나무상자");
        nameData.Add(2000000000, "책상");
        nameData.Add(356000000000, "루도의 집열쇠");

            /*
                Add 함수를 사용하여 대화 데이터 입력 추가
                Npc 초상화의 표정을 바꿀 때 사용하는 구분자는 "|"(Vertical Bar 두개)로 하기로 함.
                구분자와 함께 초상화 Index를 문장 뒤에 추가

                portaitArr <- 초상화 Sprite 배열

                아무것도 없을 시 >>
                0 : 투명함

                Luna >>
                1 : 기본
                2 : 생각
                3 : 웃음
                4 : 화남

                Ludo >>
                5 : 기본
                6 : 생각
                7 : 웃음
                8 : 화남

                나무 상자 >>
                8 : 기본

                나무 책상 >> 
                9 : 기본
            */

            /*
             code 
             종류  - 고정오브젝트,Character, Item
             메인퀘스트 
              서브퀘스트
                대화 인덱스
             */


            //현재 상태 
            // 메인퀘스트 상태

            // 메인퀘스트 상태로 다음 메인퀘스트를 알 수 있다.
            // 서브퀘스트는 현재의 상태로 체크해서 시작할 수 있는 여부를 체크할 수 있다.
            //메인퀘스트 시작조건
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


            //메인퀘스트 1 번 이벤트이다.
            // 보상은 뭐다
            // 이다음은 2번 열린다.


            /*
             * 1번이벤트
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
             * 
             * 
             이벤트 코드
             대사 하는 주체
             주체의 상태- 나오는 포트레이트의 감정 등
                인덱스
                대사 내용


             */



            //  << Default Talk >>
            talkData.Add(700000000000, new string[] {"넌 누구지?|5", 
                                    "처음 보는 얼굴인데..|6",
                                    "아무튼 신경쓰이니깐 더이상 볼일이 없다면 저리가 줄래?|8"});
        talkData.Add(701000000000, new string[] {   "안녕?|1",
                                            "이 곳에 처음 왔구나? 반가워!|3",
                                            "내 이름은 루나야!|2",
                                            "잘부탁해!|3"});
        talkData.Add(1000000000, new string[] {    "평범한 나무상자다.|9" });
        talkData.Add(2000000000, new string[] {    "누군가 사용했던 흔적이 있는 책상이다.|10" });
        
        //  << Quest Talk >>
        //퀘스트번호 + NPC Id에 해당하는 대화 데이터 작성

        // Main Quest
        talkData.Add(1000 + 701000000000, new string[] {  "어서와.|1",
                                                "그거알아?|1",
                                                "이 마을에 놀라운 전설이 있데!|2",
                                                "자세한 건 왼쪽에 있는 루도가 알려줄꺼야.|1"});

        talkData.Add(1001 + 700000000000, new string[] {  "...뭐야?|5",
                                                "루나가 나에게 오라고 했다고?|6",
                                                "전설을.. 알려 달라고?|5",
                                                "맨입에 알려주긴 그렇고...|8",
                                                "일 하나만 해줬으면 하는데..|6",
                                                "마침 내가 집 열쇠를 잃어버렸거든?|6",
                                                "호수 근처에 떨어뜨린 거 같은데..|5",
                                                "그 열쇠를 찾아줬으면 해.|5"});
        talkData.Add(2000 + 701000000000, new string[] {  "루도의 집열쇠?|2",
                                                "그런 중요한 걸 흘리고 다니면 못쓰지!|4",
                                                "나중에 루도에게 한마디 해야겠어!|4"});
        talkData.Add(2000 + 700000000000, new string[] {  "얼른 찾아오기나 해.|5"});
        talkData.Add(2000 + 356000000000, new string[] {  "조금 전에 떨어진 듯한 열쇠를 발견했다." });

        talkData.Add(2001 + 700000000000, new string[] {  "오, 찾아오다니.. 생각보다 쓸만한 놈인걸?|5" });
        talkData.Add(3000 + 701000000000, new string[] {  "왜? 무슨일 있어?|1" });
        talkData.Add(3000 + 700000000000, new string[] {  "더 이상은 귀찮으니까 말 걸지 말아줄래?|6" });

        // Sub Quest
        talkData.Add(1000000 + 1000000000, new string[] { "새로운 서브 퀘스트가 생겼습니다. \"꼬마 손님 10명 받기.\"|9",
                                                "책상을 살펴보자.|0"});
        talkData.Add(2000000 + 1000000000, new string[] { "새로운 서브 퀘스트가 생겼습니다. \"분리수거 10번 하기.\"|9",
                                                "책상을 살펴보자.|0}"});
        talkData.Add(3000000 + 1000000000, new string[] { "새로운 서브 퀘스트가 생겼습니다. \"낚시터 청소 10번 하기.\"|9",
                                                "책상을 살펴보자.|0}"});
        talkData.Add(1000000 + 2000000000, new string[] { "\"꼬마 손님 10명이 왔다감\" - 최신우|10",
                                                "다시 나무상자로 돌아가보자|0"});
        talkData.Add(2000000 + 2000000000, new string[] { "\"분리수거 10번 함\" - 최신우|10",
                                                "다시 나무상자로 돌아가보자|0"});
        talkData.Add(3000000 + 2000000000, new string[] { "\"낚시터 청소 10번 함\" - 최신우|10",
                                                "다시 나무상자로 돌아가보자|0"});

        //  << Portrait Data >>

        portraitData.Add(0, portaitArr[0]);
        portraitData.Add(701000000000 + 1, portaitArr[1]);
        portraitData.Add(701000000000 + 2, portaitArr[2]);
        portraitData.Add(701000000000 + 3, portaitArr[3]);
        portraitData.Add(701000000000 + 4, portaitArr[4]);
        portraitData.Add(700000000000 + 5, portaitArr[5]);
        portraitData.Add(700000000000 + 6, portaitArr[6]);
        portraitData.Add(700000000000 + 7, portaitArr[7]);
        portraitData.Add(700000000000 + 8, portaitArr[8]);
        portraitData.Add(1000000000 + 9, portaitArr[9]);
        portraitData.Add(2000000000 + 10, portaitArr[10]);

    }
    //대화의 대상을 반환하는 함수
    public string GetName(long id)
    {
        if (talkData == null)
            return null;
        else
            return nameData[id];
    }
    int looptest=0;
    //지정된 대화 문장을 반환하는 함수 하나 생성
    public string GetTalk(long id, int talkindex) // 매개변수 : id 값과 몇번째 문장을 들고 올 것인지
    {
        //해당 퀘스트 진행 순서 대사가 없을 때.
        //퀘스트 맨 처음 대사를 가지고 온다.
        /*
        <<id값 성분표 (12자리)>>
        111 222 333 444 > 
        [1] [고정 오브젝트(000 ~ 350), 아이템(356~699) , 캐릭터 및 NPC(700~999) + 000000000] 
        [2] 서브퀘스트 
        [3] 메인퀘스트 
        [4] 대화
        <<id 예외 처리 참고표>>
        1. id :                                                111 222 333 444 // 처음 받은 id x
        2. id - id % 1000000000 :                              111 XXX XXX XXX // 기본 대화
        3. id - id % 1000 :                                    111 222 333 XXX // 대화를 초기화 x
        4. id - id % 1000000 :                                 111 222 XXX XXX // 서브 퀘스트만
        5. id - id % 1000000 + id % 1000 :                     111 222 XXX 444 // 서브 퀘스트 대화
        6. id - id % 1000000000 + id % 1000000 - id % 1000 :   111 XXX 333 XXX // 메인 퀘스트만
        7. id - id % 1000000000 + id % 100000 :                111 XXX 333 444 // 메인퀘스트 대화
        
        기본 id > 기본 id - talkindex > 메인퀘스트 > 서브퀘스트 > 
        메인퀘스트만 > 서브퀘스트만 > npc 기본 대화 > 에러 메세지

        id > id - id % 1000 > id - id % 1000000000 + id % 1000000 > id - id % 1000000 + id % 1000 >
        id - id % 1000000000 + id % 1000000 - id % 1000 > id - id % 1000000 > return "id error"
        */
        //  <<예외 처리>>
        //ContainsKey() : Dictionary에 Key가 존재하는지 검사
        looptest++;
        if (!talkData.ContainsKey(id)) { // 1 1 1 1
            if (!talkData.ContainsKey(id - id % 1000)) { // 1 1 1 0
                if (!talkData.ContainsKey(id - id % 1000000000 + id % 1000000)) { 
                    if (!talkData.ContainsKey(id - id % 1000000 + id % 1000)) {
                        if (!talkData.ContainsKey(id - id % 1000000000 + id % 1000000 - id % 1000)) {
                            if (!talkData.ContainsKey(id - id % 1000000)) {
                                if (!talkData.ContainsKey(id - id % 1000000000)) {
                                    UnityEngine.Debug.Log("error id 값 : " + id);
                                    UnityEngine.Debug.Log("Loop Test" + looptest);
                                    looptest = 0;
                                    return "id error"; }
                                else {
                                    UnityEngine.Debug.Log("//npc 기본 대화");
                                    return GetTalk(id - id % 1000000000, talkindex); } }
                            else {
                                UnityEngine.Debug.Log("//서브퀘스트 - talkindex");
                                return GetTalk(id - id % 1000000, talkindex); } }
                        else {
                            UnityEngine.Debug.Log("//메인퀘스트 - talkindex");
                            return GetTalk(id - id % 1000000000 + id % 1000000 - id % 1000, talkindex); } }
                    else {
                        UnityEngine.Debug.Log("//서브퀘스트 + talkindex");
                        return GetTalk(id - id % 1000000 + id % 1000, talkindex); } }
                else {
                    UnityEngine.Debug.Log("//메인퀘스트 + talkindex"); //o1 ^^1발 ㅅH771 때문에 안되네
                    return GetTalk(id - id % 1000000000 + id % 1000000, talkindex); } }
            else {
                UnityEngine.Debug.Log("//메인퀘스트와 서브퀘스트가 겹침 - talkindex");
                return GetTalk(id - id % 1000, talkindex); } }
        UnityEngine.Debug.Log("id 값 : " + id);
        //talkIndex와 대화의 문장 개수를 비교하여 끝을 확인
        if (talkindex == talkData[id].Length)
            return "";
        else
            return talkData[id][talkindex];  //id로 대화 Get -> talkIndex로 대화의 한 문장의 Get
    }
    //지정된 초상화 스프라이트를 반환할 함수 생성
    public Sprite GetPortrait(long id, int portraitIndex)
    {
        if(portraitIndex == 0)
            return portraitData[0];
        return portraitData[id + (long)portraitIndex];
    }

}
