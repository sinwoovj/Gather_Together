using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 프로그래밍 전에 꼭 먼저 UnityEngine.UI를 넣어야함.
using UnityEngine.SceneManagement; // 씬 이동

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager; //대화 매니저를 변수로 선언 후, 함수 사용
    public QuestManager questManager;
    public Animator talkPanel; //패널 애니메이션 숨기기 & 보여주기
    public Animator portraitAnim; //초상화 애니메이션
    public Image portraitImg; //Image UI 접근을 위해 변수 생성 & 할당
    public Sprite prevPortrait; //비교용 과거 스프라이트 저장 변수
    public TypeEffect talk; //기존 사용하던 Text 변수를 작성한 이펙트 스크립트로 변경
    public Text mainQuestText;
    public Text subQuestText;
    public GameObject SubQuestPanel;
    public GameObject menuSet;
    public GameObject scanObject; //플레이어가 조사를 했던 오브젝트
    public GameObject player;
    public Text Objectname;
    public bool isAction; //상태 저장용 변수
    public int talkIndex;

    void Start()
    {
        //데이터 로드
        GameLoad();
        mainQuestText.text = questManager.CheckQuest();
        subQuestText.text = questManager.CheckSubQuest();
    }

    private void Update()
    {
        //Sub Menu

        //ESC키를 누르면 메뉴가 나오도록 작성
        if (Input.GetButtonDown("Cancel")){
            //ESC키로 켜고 끄기 가능하도록 작성
            if (menuSet.activeSelf)
            {
                isAction = false;
                menuSet.SetActive(false);
                SubQuestPanel.SetActive(true);
            }
                
            else
            {
                isAction = true;
                menuSet.SetActive(true);
                SubQuestPanel.SetActive(false);
            }
                
        }
    }
    public void Action(GameObject scanObj)
    { 
        //Get Current Object
        scanObject = scanObj;
        ObjData objData = scanObj.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        //Visible Talk for Action
        talkPanel.SetBool("isShow", isAction);
    }
    
    void Talk(long id, bool isNpc)
    {
        //Set Talk Data
        string talkData = "";
        int QuestTalkIndex = 0;
        string nameData = "";

        if (talk.isAnim){  //매니저에서도 플래그 변수를 이용하여 분기점 로직 작성
            talk.SetMsg("");
            return;
        }

        else
        {
            QuestTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + (long)QuestTalkIndex, talkIndex);
            nameData = talkManager.GetName(id);

            //Set Name
            Objectname.text = nameData;
        }

        //Id Exception
        if (talkData == "id error")
        {
            Debug.Log("id error");
            isAction = false;
            talkIndex = 0;
            return;
        }

        //End Talk
        if (talkData == "")
        {
            Debug.Log("End Talk");
            isAction = false;
            talkIndex = 0; //talkIndex는 대화가 끝날 때 0으로 초기화
            
            //서브/메인 퀘스트 id가 마지막에 해당될 때
            if(questManager.QuestId == 3000){
                Debug.Log("Initialization");
                questManager.QuestId = 0;
                questManager.QuestActionIndex = 0;
            }
            else
                mainQuestText.text = questManager.CheckQuest(id);
            if(questManager.SubQuestId == 4000000){
                questManager.SubQuestId = 0;
                questManager.QuestActionIndex = 0;
            }
            else
                subQuestText.text = questManager.CheckSubQuest(id);
            return; //void 함수에서 return은 강제 종료 역할
        }
        isAction = true;
        ++talkIndex; //다음 문장이 나오도록 인덱스를 1 증가

        //Continue Talk
        if (isNpc)
        {
            //구분자를 사용해 분리하고 배열로 만들어지면 배열의 0번째인 원래 문장으로 설정
            talk.SetMsg(talkData.Split('|')[0]);

            //Show Protrait

            // talkData.Split('|')[1]는 string 배열이기에 형변환을 시켜주어야한다.
            // Parse() : 문자열을 해당 타입으로 변환해주는 함수 [속칭 > 파싱한다.]
            // Parse()는 문자열 내용이 타입과 맞지 않으면 오류가 발생한다.
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split('|')[1]));

            //NPC일 때만 Image가 보이도록 작성 (R, G, B, 투명도)
            portraitImg.color = new Color(1, 1, 1, 1);

            //Animation Portrait

            //과거 스프라이트를 저장해두어 비교 후, 애니메이션 실행 
            if(prevPortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
        }

        else
        {
            talk.SetMsg(talkData);

            //NPC일 때만 Image가 보이도록 작성 (R, G, B, 투명도)
            portraitImg.color = new Color(1, 1, 1, 0);
        }
    }

    public void GameSave() //저장 함수
    {
        //PlayerPrefs : 간단한 데이터 저장 기능을 지원하는 클래스 // 데이터 타입에 맞게 Set 함수 사용 (SetInt/float/string)

        //플레이어 위치 값
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x); //player.x
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y); //player.y

        //퀘스트 진행도

        PlayerPrefs.SetInt("QuestId", questManager.QuestId);
        PlayerPrefs.SetInt("SubQuestId", questManager.SubQuestId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.QuestActionIndex);

        //저장
        PlayerPrefs.Save();

        //메뉴 비활성화
        menuSet.SetActive(false);
    }
    public void GameLoad() //불러오기 함수
    {
        //최초 게임 실행했을 땐 데이터가 없으므로 예외처리 로직 작성
        if (!PlayerPrefs.HasKey("PlayerX") || !PlayerPrefs.HasKey("PlayerY") || 
            !PlayerPrefs.HasKey("QuestId") || !PlayerPrefs.HasKey("SubQuestId") || 
            !PlayerPrefs.HasKey("QuestActionIndex"))
            return;

        //불러오기 또한 데이터 타입에 맞게 Get 함수 사용

        //플레이어 위치 값
        float x = PlayerPrefs.GetFloat("PlayerX"); //player.x
        float y = PlayerPrefs.GetFloat("PlayerY"); //player.y


        //퀘스트 진행도

        int QuestId = PlayerPrefs.GetInt("QuestId"); ; //Quest Id
        int QuestActionIndex = PlayerPrefs.GetInt("SubQuestId");; //QUest Action Index
        int SubQuestId = PlayerPrefs.GetInt("QuestActionIndex");; //sub Quest Id

        
        
        //세팅 : 불러온 데이터를 게임 오브젝트에 적용
        player.transform.position = new Vector3(x, y, 0);
        questManager.QuestId = QuestId;
        questManager.SubQuestId = SubQuestId;
        questManager.QuestActionIndex = QuestActionIndex;
        questManager.ControlObject(); //불러오기 했을 당시의 퀘스트 순서와 연결된 오브젝트 관리 추가
        questManager.SubControlObject();
    }

    public void GoMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void CloseTalk()
    {
        Debug.Log("스킵");
        talkIndex = 0; //talkIndex는 대화가 끝날 때 0으로 초기화
        isAction = false;
        talkPanel.SetBool("isShow", isAction);
    }
}
