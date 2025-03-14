using UnityEngine;

public class SprayBottle : MonoBehaviour
{
    public ParticleSystem sprayParticles;

    void Start()
    {
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
        if (Input.GetMouseButtonDown(2))
        {
            StartSpray();
        }

        if (Input.GetMouseButtonUp(2))
        {
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