using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public static bool isSaving;
    public GameObject SaveSettingValues;
    public void Awake()
    {
        isSaving = false;
    }
    public void Gamestart()
    {
        StartCoroutine(GameStartIE());
    }

    IEnumerator GameStartIE()
    {
        GameObject.Find("FadeIn").GetComponent<FadeIn>().FadeInOn = true;
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Game");
        DontDestroyOnLoad(SaveSettingValues);
    }
}
