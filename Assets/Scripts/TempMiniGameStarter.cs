using DI;
using UnityEngine;

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
