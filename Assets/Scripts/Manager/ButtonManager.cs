using DI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonManager : DIMono
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
    public void SelectionButton()
    {
        GameObject clickObject =
            EventSystem.current.currentSelectedGameObject;
        playData.selectNumber = int.Parse(clickObject.name[9].ToString());
        Debug.Log(playData.selectNumber);
    }
    public void OpenQuestList()
    {

    }
}
