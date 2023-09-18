
using UnityEngine;
using DI;

using UnityEngine.AddressableAssets;

public class Bootstrap
{

    [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void ReadyToProject()
    {

        var gameData=Addressables.LoadAssetAsync<GameData>("data.asset").WaitForCompletion();

        DIContainer.Global.Regist(gameData);
        DIContainer.Global.Regist(new PlayData());
        DIContainer.Global.Regist(new UserData());
       // DIContainer.Global.Regist(new PlayData());
    }
}