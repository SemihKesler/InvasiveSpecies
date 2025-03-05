using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compost : MonoBehaviour
{
    public AudioClip compostSound;
    public AudioSource machine;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<plant>() != null)
        {
            machine.PlayOneShot(compostSound);
            Destroy(collision.gameObject);
        }
    }
}
