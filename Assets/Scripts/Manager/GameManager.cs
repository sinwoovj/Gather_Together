using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 프로그래밍 전에 꼭 먼저 UnityEngine.UI를 넣어야함.
using UnityEngine.SceneManagement; // 씬 이동
using static ObjData;
using DI;
using System.Linq;
using System.IO;

public class GameManager : DIMono
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


    [Inject]
    PlayData playData;

    [Inject]
    GameData gameData;

    [Inject]
    UserData userData;

    void CheckSceneFromMiniGame()
    {
        switch (playData.fromMiniGame)
        {
            case PlayData.FromMiniGame.None:
                break;
            case PlayData.FromMiniGame.BugManager:
//                playData.miniGameScore;

                break;
            case PlayData.FromMiniGame.FishManager:
                break;
        }
    }


    protected override void Initialize()
    {
        //데이터 로드
        Debug.Log("Data Load");
        GameLoad();
        Debug.Log("MainQuestId : " + userData.MainQuestId.ToString() + "  SubQuestId : " + userData.SubQuestId.ToString());
        mainQuestText.text = questManager.CheckMainQuestData() == null ? "" : questManager.CheckMainQuestData().MainQuestName;
        subQuestText.text = questManager.CheckSubQuestData() == null ? "" : questManager.CheckSubQuestData().SubQuestName;
        CheckSceneFromMiniGame();
    }

    private void Update()
    {
        //ESC키를 누르면 메뉴가 나오도록 작성
        if (Input.GetButtonDown("Cancel")){
            //ESC키로 켜고 끄기 가능하도록 작성
            if (menuSet.activeSelf)
            {
                isAction = false;
                menuSet.SetActive(false);
                SubQuestPanel.SetActive(true);
            }else{
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

        var scenarioData= objData.GetCurrentScenarioData();
        switch (scenarioData.ScenarioActionType)
        {
            case ScenarioData.scenarioActionType.ToUnityScene:
                break;
            case ScenarioData.scenarioActionType.ToStartScene:
                break;
            case ScenarioData.scenarioActionType.LikeabilityCondition:
                break;
            case ScenarioData.scenarioActionType.StatCondition:
                break;
            case ScenarioData.scenarioActionType.Talk:

                break;

        }

        //Visible Talk for Action
        talkPanel.SetBool("isShow", isAction);
    }


    public void GameSave() //저장 함수
    {
        //PlayerPrefs : 간단한 데이터 저장 기능을 지원하는 클래스 // 데이터 타입에 맞게 Set 함수 사용 (SetInt/float/string)

        userData.FillUserPostion(player.transform);
        var json = UnityEngine.JsonUtility.ToJson(userData);


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
        var userData1 = UnityEngine.JsonUtility.FromJson<UserData>(json);
        this.userData.CopyFrom(userData1);

        //세팅 : 불러온 데이터를 게임 오브젝트에 적용
        player.transform.position = userData1.PlayerLoc;
    }
}
