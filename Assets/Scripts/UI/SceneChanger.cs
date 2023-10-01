using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public  class SceneChanger
{
    public string targetSceneName;
    public string loaidngSceneName;
    public string fromSceneName;

    public enum LoadingScene
    {
        FadeInOut
    }

    public string GetLoadingSceneName(LoadingScene loadingScene)
    {
        switch (loadingScene)
        {
            case LoadingScene.FadeInOut:
                return "FadeInOut";
        }
        return "";
    }


    public void ChangeScene(string targetScene, LoadingScene loadingScene)
    {
        this.fromSceneName = SceneManager.GetActiveScene().name;
        this.loaidngSceneName = GetLoadingSceneName(loadingScene);
        SceneManager.LoadScene(loaidngSceneName, LoadSceneMode.Additive);

        this.targetSceneName = targetScene;

    }

    
}