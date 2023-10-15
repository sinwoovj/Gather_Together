using UnityEngine;
using UnityEngine.UI;

public class DiaMask : MonoBehaviour
{
    public Material material;

    [Range(0,1)]
    public float _progress;

    public float Progress
    {
        set
        {        

            material.SetFloat("_Progress", value);
        }
        get { return material.GetFloat("_Progress"); }
    }

    private void Update()
    {
        Progress= Mathf.Clamp01(_progress); 
    }
}
