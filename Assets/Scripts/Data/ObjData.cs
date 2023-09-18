using DI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjData : DIMono
{
    [Inject]
    UserData userData;

    [Inject]
    GameData gameData;
    public int id; //오브젝트의 ID


    public ScenarioData.hostType hostType;


    public ScenarioData  GetCurrentScenarioData()
    {
        var scenarioData= gameData.ScenarioData.Where(l => l.Id == id && l.HostType == hostType);

        var currentSD= scenarioData.FirstOrDefault(l => l.MainQuestId == userData.MainQuestId && l.SubQuestId == userData.SubQuestId);

        if(currentSD != null)
        {
            return currentSD;
        }
        currentSD = scenarioData.FirstOrDefault(l => l.MainQuestId == userData.MainQuestId );

        if (currentSD != null)
        {
            return currentSD;
        }

        return scenarioData.FirstOrDefault(l => l.MainQuestId == 0 && l.SubQuestId==0);

    }

}
