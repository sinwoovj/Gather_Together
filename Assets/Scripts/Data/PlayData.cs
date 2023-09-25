using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayData 
{
    public int presentChar;
    public bool questDetective;
    public bool isAction;
    public bool isClickSuccess;
    public int selectNumber;
    public float miniGameScore;
    public float score;


    public enum FromMiniGame
    {
        None,
        BugManager,
        FishManager
    }

    public FromMiniGame fromMiniGame = FromMiniGame.None;
    public bool NeedSkip;
}
