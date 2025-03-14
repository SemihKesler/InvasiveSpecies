using UnityEngine;

public class SprayBottlev2 : MonoBehaviour
{
    public ParticleSystem sprayParticles;

    void Update()
    {
        // Check for middle mouse button click
        if (Input.GetMouseButtonDown(2)) // 2 is the middle mouse button
        {
            // Emit a burst of particles
            sprayParticles.Emit(10); // Emit 10 particles
        }
    }
}
