using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bugclick : MonoBehaviour
{
    private bugManager manager;

    void Start()
    {
        manager = FindObjectOfType<bugManager>();
    }

    void OnMouseDown()
    {
        if (manager.isAtSecondPosition)
        {
            manager.RemoveObject(gameObject);
        }
    }
}
