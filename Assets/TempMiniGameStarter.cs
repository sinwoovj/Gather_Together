using DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempMiniGameStarter : DIMono
{
    [Inject]
    PlayData playData;


    protected override void Initialize()
    {
        if(playData.fromMiniGame!= PlayData.FromMiniGame.None)
        {
            ApplyMinigameResult();
            return;
        }

    }

    private void ApplyMinigameResult()
    {
        Debug.Log("From " + playData.fromMiniGame + " Score :"+playData.miniGameScore);
    }

}
