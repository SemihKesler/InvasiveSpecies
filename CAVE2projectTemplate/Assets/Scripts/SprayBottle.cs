using UnityEngine;

public class SprayBottle : MonoBehaviour
{
    public ParticleSystem sprayParticles;

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
        sprayParticles.Play();
    }

    void StopSpray()
    {
        sprayParticles.Stop();
    }
}