using UnityEngine;
using UnityEngine.UI; // UI 프로그래밍 전에 꼭 먼저 UnityEngine.UI를 넣어야함.
using DI;
using UnityEngine.SceneManagement;

public class GameManager : DIMono
{
    public TalkManager talkManager; //대화 매니저를 변수로 선언 후, 함수 사용
    public QuestManager questManager;
    public Text mainQuestText;
    public Text subQuestText;
    public GameObject SubQuestPanel;
    public GameObject menuSet;
    public Text Objectname;
    


    [Inject]
    PlayData playData;

    [Inject]
    GameData gameData;

    [Inject]
    UserData userData;

    [Inject]
    DataManager dataManager;

    void CheckSceneFromMiniGame()
    {
        switch (playData.fromMiniGame)
        {
            case PlayData.FromMiniGame.None:
                break;
            case PlayData.FromMiniGame.BugManager:
                break;
            case PlayData.FromMiniGame.FishManager:
                break;
        }
    }

    protected override void Initialize()
    {
        //데이터 로드
        dataManager.GameLoad();
        playData.isAction = false;
        mainQuestText.text = questManager.CheckMainQuestData() == null ? "" : questManager.CheckMainQuestData().MainQuestName;
        subQuestText.text = questManager.CheckSubQuestData() == null ? "" : questManager.CheckSubQuestData().SubQuestName;
    }

    private void Update()
    {
        //ESC키를 누르면 메뉴가 나오도록 작성
        if (Input.GetButtonDown("Cancel")){
            //ESC키로 켜고 끄기 가능하도록 작성
            if (menuSet.activeSelf)
            {
                playData.isAction = false;
                menuSet.SetActive(false);
                SubQuestPanel.SetActive(true);
            }else{
                playData.isAction = true;
                menuSet.SetActive(true);
                SubQuestPanel.SetActive(false);
            }
        }
        if (playData.questDetective)
        {
            mainQuestText.text = questManager.CheckMainQuestData() == null ? "" : questManager.CheckMainQuestData().MainQuestName;
            subQuestText.text = questManager.CheckSubQuestData() == null ? "" : questManager.CheckSubQuestData().SubQuestName;
            playData.questDetective = false;
        }
    }

    public void Action(GameObject scanObj)
    {
        ObjData objData = scanObj.GetComponent<ObjData>();
        var scenarioData = objData.GetCurrentScenarioData();
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
                StartCoroutine(talkManager.StartScene(scenarioData.IntVal));
                break;

        }
    }
}
