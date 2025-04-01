using UnityEngine;

public class SprayBottle : MonoBehaviour
{
    public ParticleSystem sprayParticles;
    bool changeState = false;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();

        if (sprayParticles == null)
        {
            Debug.LogError("SprayParticles is not assigned in the Inspector!");
        }
        else
        {
            Debug.Log("SprayParticles is assigned correctly.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2) || CAVE2.GetButtonDown(CAVE2.Button.ButtonUp))
        {
            if (audio != null)
            {
                audio.Play();
            }
            StartSpray();
        }
        if (Input.GetMouseButtonUp(2) || CAVE2.GetButtonUp(CAVE2.Button.ButtonUp))
        {
            if (audio != null)
            {
                audio.Pause();
            }
            StopSpray();
        }
    }

    void StartSpray()
    {
        Debug.Log("Spray started");
        sprayParticles.Play();
    }

    void StopSpray()
    {
        Debug.Log("Spray stopped");
        sprayParticles.Stop();
    }
}