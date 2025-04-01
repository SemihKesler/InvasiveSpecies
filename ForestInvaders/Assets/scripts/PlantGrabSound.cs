using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrabSound : MonoBehaviour
{
    public AudioClip grabSound;           
    private AudioSource audioSource;      
    private GrabbableObject grabScript;   
    private bool hasPlayed = false;      

    void Start()
    {
        grabScript = GetComponent<GrabbableObject>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        if (grabScript != null && grabScript.getGrabbed() && !hasPlayed)
        {
            if (grabSound != null)
            {
                audioSource.PlayOneShot(grabSound);
            }

            hasPlayed = true;
        }
    }
}
