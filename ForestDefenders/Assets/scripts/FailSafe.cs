using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FailSafe : MonoBehaviour
{
    public ToolSwitch toolSwitch;
    public GameObject toolSpawn;

    public GameObject player;
    private Vector3 currPos;
    private Vector3 setSpawn = Vector3.zero;
    private Quaternion setRot = Quaternion.identity;
    private static GameObject lastGrabbed;
    private static GrabbableObject lastGrabbable;
    private static GameObject prevGrabbed;
    private bool pulling = false;
    private float pullSpeed = 0.2f;

    public void setLastGrabbed(GameObject grabbed, GrabbableObject script)
    {
        lastGrabbed = grabbed;
        lastGrabbable = script;
    }

    void resetSpawn()
    {
        if (player != null)
        {
            player.transform.position = setSpawn;
            player.transform.rotation = setRot;
        }
    }
    
    void Update()
    {
        //Allow pulling of invasive plants
        if (lastGrabbed != prevGrabbed)
        {
            Debug.Log(lastGrabbed);
            prevGrabbed = lastGrabbed;
        }

        if (lastGrabbed != null)
        {
            if (Input.GetKeyDown(KeyCode.P) || CAVE2.GetButtonDown(CAVE2.Button.ButtonDown))
            {
                if (lastGrabbable != null)
                {
                    if (lastGrabbable.getGrabbed())
                    {
                        lastGrabbable.setGrabbed(false);
                        lastGrabbable.release();
                    }
                }
                pulling = true;
            }

            if (Input.GetKeyUp(KeyCode.P) || CAVE2.GetButtonUp(CAVE2.Button.ButtonDown))
            {
                pulling = false;
            }

            if (pulling)
            {
                lastGrabbed.transform.position = Vector3.MoveTowards(lastGrabbed.transform.position, toolSpawn.transform.position, pullSpeed);
            }
        }

        //Reset to spawn if fall off map
        if (player != null)
        {
            currPos = player.transform.position;
            if (currPos.y < -10f)
            {
                resetSpawn();
            }
        }

        //Respawns axe infront of player
        if (toolSwitch != null && toolSpawn != null && Input.GetKeyDown(KeyCode.T) || CAVE2.GetButtonDown(CAVE2.Button.ButtonLeft))
        {
            GameObject tool = toolSwitch.getCurrentTool();
            tool.transform.position = toolSpawn.transform.position;
        }
    }
}
