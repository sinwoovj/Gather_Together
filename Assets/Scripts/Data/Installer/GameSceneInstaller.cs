using DI;
using UnityEngine;

public class GameSceneInstaller : MonoBehaviour
{
    // Manager
    public GameManager GameManager;
    public DataManager DataManager;
    public QuestManager QuestManager;
    public ChatManager ChatManager;
    public TalkManager TalkManager;
    public SelectManager SelectManager;

    public GameObject Player;

    private void Awake()
    {
        var container = new DIContainer();
        DIContainer.Local = container;

        // Manager
        container.Regist(GameManager);
        container.Regist(DataManager);
        container.Regist(QuestManager);
        container.Regist(ChatManager);
        container.Regist(TalkManager);
        container.Regist(SelectManager);

        // Object
        container.Regist(Player);
    }

}
