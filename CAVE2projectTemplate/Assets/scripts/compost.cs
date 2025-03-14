using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compost : MonoBehaviour
{
    public AudioClip compostSound;
    public AudioSource machine;
    public Text fact;

    void Start()
    {
        fact.text = "";
    }



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InvasivePlant"))
        {
            PlantFact plant = collision.gameObject.GetComponent<PlantFact>();

            fact.text = plant.fact;
            machine.PlayOneShot(compostSound);
            Destroy(collision.gameObject);
        }
    }
}
