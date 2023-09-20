using DI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneInstaller : MonoBehaviour
{
    public GameManager GameManager;
    public DataManager DataManager;
    public GameObject Player;
    public QuestManager QuestManager;

    private void Awake()
    {
        var container = new DIContainer(); ;
        DIContainer.Local = container;

        container.Regist(GameManager);
        container.Regist(DataManager);
        container.Regist(Player);
        container.Regist(QuestManager);
    }

}
