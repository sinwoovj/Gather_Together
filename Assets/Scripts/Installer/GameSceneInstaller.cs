using DI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneInstaller : MonoBehaviour
{
    public GameManager GameManager;

    private void Awake()
    {
        var container = new DIContainer(); ;
        DIContainer.Local = container;

        container.Regist(GameManager);
    }

}
