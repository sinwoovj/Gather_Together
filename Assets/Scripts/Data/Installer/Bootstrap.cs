using DI;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Bootstrap
{
    [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void ReadyToProject()
    {
        var gameData = Addressables.LoadAssetAsync<GameData>("data.asset").WaitForCompletion();
        var userData = new UserData();

        DIContainer.Global.Regist(gameData);
        DIContainer.Global.Regist(new PlayData());
        DIContainer.Global.Regist(userData);
        UserDataAsset userDataAsset = ScriptableObject.CreateInstance<UserDataAsset>();
        userDataAsset.CopyFrom(userData); // 사용자 데이터 복사
        DIContainer.Global.Regist(SettingData.LoadSettingData());
        DIContainer.Global.Regist(new SceneChanger());
    }
}