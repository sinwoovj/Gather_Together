using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeySettingValue.keys[StandByKeyAction.UP]))
            Debug.Log("Up");
        else if (Input.GetKey(KeySettingValue.keys[StandByKeyAction.DOWN]))
            Debug.Log("DOWN");
        if (Input.GetKey(KeySettingValue.keys[StandByKeyAction.RIGHT]))
            Debug.Log("RIGHT");
        else if (Input.GetKey(KeySettingValue.keys[StandByKeyAction.LEFT]))
            Debug.Log("LEFT");
        if (Input.GetKey(KeySettingValue.keys[StandByKeyAction.INTERACTION]))
            Debug.Log("INTERACTION");
    }
}
