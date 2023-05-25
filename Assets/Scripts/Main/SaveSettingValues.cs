using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveSettingValues : MonoBehaviour
{
    KeyManager keyManager;
    public string UpKeySettingValue;
    public string DownKeySettingValue;
    public string LeftKeySettingValue;
    public string RightKeySettingValue;
    public string interactionKeySettingValue;
    void Awake()
    {
        keyManager = GameObject.Find("Key").GetComponent<KeyManager>();
    }

    void Update()
    {
        UpKeySettingValue = (keyManager.defaultKeys[0]).ToString(); //Up
        DownKeySettingValue = (keyManager.defaultKeys[1]).ToString(); //Down
        LeftKeySettingValue = (keyManager.defaultKeys[2]).ToString(); //Left
        RightKeySettingValue = (keyManager.defaultKeys[3]).ToString(); //Right
        interactionKeySettingValue = (keyManager.defaultKeys[4]).ToString(); //Interaction
    }
}
