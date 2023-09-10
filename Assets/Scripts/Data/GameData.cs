using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GameData : ScriptableObject
{
    public List<NPC> NPC = new List<NPC>();
    public List<MainQuest> MainQuest = new List<MainQuest>();
    public List<SubQuest> SubQuest = new List<SubQuest>();
    public List<GameScene> GameScene = new List<GameScene>();
    public List<HostImage> HostImage = new List<HostImage>();
    public List<SceneSelection> SceneSelection = new List<SceneSelection>();
    public List<SceneCondition> SceneCondition = new List<SceneCondition>();
    public List<SceneLine> SceneLine = new List<SceneLine>();

}
