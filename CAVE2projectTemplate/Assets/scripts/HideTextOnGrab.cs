using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTextOnGrab : MonoBehaviour
{
    public GameObject textBoxToHide;
    private GrabbableObject grabScript;
    private bool alreadyHidden = false;

    void Start()
    {
        grabScript = GetComponent<GrabbableObject>();
    }

    void Update()
    {
        if (grabScript != null && grabScript.IsGrabbed() && !alreadyHidden && textBoxToHide.activeSelf)
        {
            textBoxToHide.SetActive(false); // Hide Text
            alreadyHidden = true;
        }
    }
}
