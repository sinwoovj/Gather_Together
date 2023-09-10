
using UnityEngine;
using DI;

public class Bootstrap
{

    

    [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void ReadyToProject()
    {

        DIContainer.Global.Regist(new PlayData());


    }
}