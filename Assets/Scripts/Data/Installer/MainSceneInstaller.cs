using DI;
using UnityEngine;

public class MainSceneInstaller : MonoBehaviour
{
    // Manager
    public SaveLoadManager SaveLoadManager;

    private void Awake()
    {
        var container = new DIContainer();
        DIContainer.Local = container;

        // Manager
        container.Regist(SaveLoadManager);

        // Object
    }

}
