using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explanation : MonoBehaviour
{
    public void GoExplanation()
    {
        Invoke("Goexplanation", 1.0f);
        GameObject.Find("FadeIn").GetComponent<FadeIn>().FadeInOn = true;
    }
    public void Goexplanation()
    {
        //SceneManager.LoadScene("Explanation");
    }
}
