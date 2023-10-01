using DI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : DIMono 
{

    [Inject]
    SceneChanger sceneChanger;
    
    protected override void Initialize()
    {
        base.Initialize();
        
    }

    public void ReadyForLoadNewScene()
    {
        StartCoroutine(LoadSceneIE());

    }

    AsyncOperation loadAsync;

    IEnumerator LoadSceneIE()
    {
        var unloadAsync= SceneManager.UnloadSceneAsync(sceneChanger.fromSceneName);
       
        while (unloadAsync.isDone==false)
        {
            yield return null;
        }

        sceneChanger.fromSceneName = null;
        loadAsync =SceneManager.LoadSceneAsync(sceneChanger.targetSceneName, LoadSceneMode.Additive);


        loadAsync.allowSceneActivation = false;
        while (loadAsync.progress <0.89f)
        {
            yield return null;
        }

        loadAsync.allowSceneActivation = true;



        this.GetComponent<Animator>().SetTrigger("LoadDone");

    }



    public void ReadyToRemoveLoadingScene()
    {


        StartCoroutine(UnloadLoadingScene());

    }

    IEnumerator UnloadLoadingScene()
    {
        yield return loadAsync;
        var unloadAsync = SceneManager.UnloadSceneAsync(sceneChanger.loaidngSceneName);

        yield return unloadAsync;


    }

}
