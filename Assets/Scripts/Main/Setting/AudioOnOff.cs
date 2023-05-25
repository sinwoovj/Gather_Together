using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioOnOff : MonoBehaviour
{
    public void AudioToggle(bool toggle)
    {
        if (toggle)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }
}
