using DI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtonManager : DIMono
{
    [Inject]
    SceneChanger sceneChanger;

    //public GameObject SaveSettingValues;
    public GameObject SettingInterface;

    void Awake()
    {
        SettingInterface.SetActive(false);
    }
    public void OpenSetting()
    {
        SettingInterface.SetActive(true);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void Gamestart()
    {
        sceneChanger.ChangeScene("Game", SceneChanger.LoadingScene.FadeInOut);
    }
    public void CloseSetting()
    {
        SettingInterface.SetActive(false);
    }
}
