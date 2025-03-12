using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPickup : MonoBehaviour
{
    public FactPopupManager factManager; // Reference to the Fact Manager (you will assign this in the Inspector)
    public string factMessage; // The message/fact to show when player picks up this plant

    // This runs when the player touches the plant (make sure the plant has a Collider with "Is Trigger" checked!)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if it was the player
        {
            factManager.ShowFact(factMessage); // Show the fact using FactPopupManager
            Destroy(gameObject); // Optional: remove the plant after it's picked up
        }
    }
}
