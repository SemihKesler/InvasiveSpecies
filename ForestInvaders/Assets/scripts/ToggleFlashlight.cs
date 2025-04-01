using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFlashlight : MonoBehaviour
{
    Light flashlight;
    bool lightState = true;

    public void lightOff()
    {
        lightState = false;
        flashlight.enabled = false;
    }

    void Start()
    {
        flashlight = GetComponent<Light>();
    }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.F) || CAVE2.GetButtonDown(CAVE2.Button.ButtonDown))
        {
            lightState = !lightState;
            flashlight.enabled = lightState;
        }
    }
}
