using DI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : DIMono
{
    [Inject]
    PlayData playData;

    [Inject]
    DataManager dataManager;
    public void GoMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void SaveGame()
    {
        dataManager.GameSave();
    }


    public void Skip()
    {
        playData.NeedSkip = true;

    }
}
