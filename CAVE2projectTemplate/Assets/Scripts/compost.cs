using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compost : MonoBehaviour
{
    public AudioClip compostSound;
    public AudioSource machine;
    public CompostSignManager signManager; // Reference to the sign manager

    void OnCollisionEnter(Collision collision)
    {
        // If a plant is composted (looking for PlantFact now)
        PlantFact plantComponent = collision.gameObject.GetComponent<PlantFact>();
        if (plantComponent != null)
        {
            machine.PlayOneShot(compostSound); // Play compost sound

            // Get the fact from the plant
            string plantFact = plantComponent.plantFact;
            signManager.UpdateSign(plantFact); // Update sign with the fact

            Destroy(collision.gameObject); // Remove plant from scene
        }
    }
}
