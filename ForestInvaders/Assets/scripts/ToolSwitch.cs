using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSwitch : MonoBehaviour
{
    public GameObject axe;
    public GameObject spray;
    bool axeParent = true;
    GrabbableObject axeGrab;
    GrabbableObject sprayGrab;
    bool axeInit = false;
    bool sprayInit = false;
    public AudioClip switchSound;
    private AudioSource axeSource;
    private AudioSource spraySource;

    void Start()
    {
        axeGrab = axe.GetComponent<GrabbableObject>();
        sprayGrab = spray.GetComponent<GrabbableObject>();
        axeSource = axe.GetComponent<AudioSource>();
        spraySource = spray.GetComponent<AudioSource>();

        if (axe == null)
        {
            Debug.Log("Axe Missing");
        }if (spray == null)
        {
            Debug.Log("Spray Missing");
        }
    }

    void switchTool()
    {
        axe.transform.SetParent(null);
        spray.transform.SetParent(null);

        if (axeParent)
        {
            axe.transform.SetParent(spray.transform);
            axe.SetActive(false);
            spray.SetActive(true);
            if (spraySource != null && switchSound != null)
            {
                spraySource.PlayOneShot(switchSound);
            }
            Debug.Log("Switched to Spray");
        }
        else
        {
            spray.transform.SetParent(axe.transform);
            spray.SetActive(false);
            axe.SetActive(true);
            if (axeSource != null && switchSound != null)
            {
                axeSource.PlayOneShot(switchSound);
            }
            Debug.Log("Switch to Axe");
        }
        axeParent = !axeParent;
    }
    void Update()
    {
        if (!axeInit)
        {
            if (axeGrab.getGrabber() != null)
            {
                axeInit = true;
            }
        }

        if (!sprayInit)
        {
            if (sprayGrab.getGrabber() != null)
            {
                sprayInit = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.Z) || CAVE2.GetButtonDown(CAVE2.Button.ButtonRight))
        {
            if (axeParent)
            {
                if (axeInit)
                {
                    axeGrab.setGrabbed(false);
                    axeGrab.release();
                    switchTool();
                }
            }
            else {
                if (sprayInit)
                {
                    sprayGrab.setGrabbed(false);
                    sprayGrab.release();
                    switchTool();
                }
            }
        }
    }
}
