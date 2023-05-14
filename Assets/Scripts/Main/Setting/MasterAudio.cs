using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MasterAudio : MonoBehaviour
{

    public AudioSource mastervolume;

    public void SetMasterVolume(float volume)
    {
        mastervolume.volume = volume;
    }
}