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
        fact.text =
@"Compost Machine:
   
    Composting Process:
    1) Accumulate Organic Materials
        (Dirt, Plants, Leaves, etc..)
    2) Heat up Compost to kill off living plants
    3) Mix Compost Well
    4) Use created compost with plant to improve growth

Insert Invasive Plants Below ↓";
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
