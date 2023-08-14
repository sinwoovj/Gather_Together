using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public static bool isSaving;
    public GameObject SaveSettingValues;
    public void Awake()
    {
        isSaving = false;
    }
    public void Gamestart()
    {
        Invoke("GameStart", 1.0f);
        GameObject.Find("FadeIn").GetComponent<FadeIn>().FadeInOn = true;
    }
    public void GameStart()
    {
        isSaving = true;
        SceneManager.LoadScene("Game");
        DontDestroyOnLoad(SaveSettingValues);
    }
}
