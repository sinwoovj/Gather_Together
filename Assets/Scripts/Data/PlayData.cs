using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayData 
{
    public bool actionBlock;
    public bool isClickSuccess;
    public float miniGameScore;
    public float score;
    public enum FromMiniGame
    {
        None,
        BugManager,
        FishManager
    }

    public FromMiniGame fromMiniGame = FromMiniGame.None;

}
