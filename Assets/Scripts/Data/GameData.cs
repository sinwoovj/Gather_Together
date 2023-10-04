using System.Collections.Generic;
using UnityEngine;

public class GameData : ScriptableObject
{
    public List<NPC> NPC;
    public List<Tower> Tower;
    public List<Item> Item;
    public List<Member> Member;
    public List<MainQuest> MainQuest;
    public List<SubQuest> SubQuest;
    public List<GameScene> GameScene;
    public List<HostImage> HostImage;
    public List<SceneSelection> SceneSelection;
    public List<SceneCondition> SceneCondition;
    public List<SceneLine> SceneLine;
    public List<ScenarioData> ScenarioData;
    public List<StatCondition> StatCondition;
    public List<LikeabilityCondition> LikeabilityCondition;
    public List<QuestCondition> QuestCondition;
   
}
