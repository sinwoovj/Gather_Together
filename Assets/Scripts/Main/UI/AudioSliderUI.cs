using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderUI : MonoBehaviour
{
    public Sprite Marsh1;
    public Sprite Marsh2;
    public Sprite Marsh3;
    public Slider slider;
    public GameObject marsh;
    void Update()
    {
        if (slider.value < 0.33)
        {
            marsh.GetComponent<Image>().sprite = Marsh1;
        }
        else if(slider.value < 0.66)
        {
            marsh.GetComponent<Image>().sprite = Marsh2;
        }
        else
        {
            marsh.GetComponent<Image>().sprite = Marsh3;
        }
    }
}
