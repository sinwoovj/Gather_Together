using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    //public Dropdown resolutionDropdown;
    List<Resolution> resolutions = new List<Resolution>();
    private void Start()
    {
        //InitUi();
    }
    void InitUi()
    {
        resolutions.AddRange(Screen.resolutions);
        foreach(Resolution item in resolutions)
        {
            Debug.Log(item.width + "x" + item.height + " " + item.refreshRate);
        }
    }
}
