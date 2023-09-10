using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayData 
{

    public bool isClickSuccess;
    public float miniGameScore;

    public enum FromMiniGame
    {
        None,
        BugManager
    }

    public FromMiniGame fromMiniGame= FromMiniGame.None;

}
