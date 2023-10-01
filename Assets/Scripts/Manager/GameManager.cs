using UnityEngine;
using DI;
using UnityEngine.SceneManagement;

public class GameManager : DIMono
{
    [Inject]
    TalkManager TalkManager; //대화 매니저를 변수로 선언 후, 함수 사용

    [Inject]
    PlayData playData;

    [Inject]
    QuestManager questManager;

    [Inject]
    DataManager dataManager;

    [Inject]
    SceneChanger changer;

    public GameObject SubQuestPanel;
    public GameObject menuSet;

    protected override void Initialize()
    {
        //데이터 로드
        dataManager.GameLoad();
        playData.isAction = false;
        playData.selectNumber = -1;
        playData.presentChar = 1;
        playData.cummutableMemberCount = 6;
        questManager.SetQuestText();
    }

    private void Update()
    {

        /// Test용
        /// 
        if (Input.GetKeyUp(KeyCode.W))
        {
            changer.ChangeScene("bug", SceneChanger.LoadingScene.FadeInOut);
        }

        ///

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
            questManager.SetQuestText();
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
                SceneManager.LoadScene(scenarioData.ActionValStr);
                break;
            case ScenarioData.scenarioActionType.ToStartScene:
                //애니메이션 같은 거나 연출
                break;
            case ScenarioData.scenarioActionType.SetStat:
                break;
            case ScenarioData.scenarioActionType.SetLikeability:
                break;
            case ScenarioData.scenarioActionType.Talk:
                StartCoroutine(TalkManager.StartScene(scenarioData.IntVal));
                break;

        }
    }

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

}
