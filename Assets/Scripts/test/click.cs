using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class click : MonoBehaviour
{
    public Button btn;
    public Slider sli;
    public int bcc = 0;
    public float timefloat = 0.0f;
    public float delay = 0.2f;
    private bool success = false;
    void Start()
    {
        btn.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        bcc++;
    }
    void Update()
    {

        if (sli.maxValue >= bcc)
            sli.value = bcc;
        else if(success == false)
        {
            Debug.Log("¼º°ø");
            success = true;
        }
        timefloat += Time.deltaTime;
        if (timefloat >= delay && success == false)
        {
            if(bcc > 0.0f)
            {
                bcc--;
            }
            timefloat = 0.0f;
        }
        
    }
}
