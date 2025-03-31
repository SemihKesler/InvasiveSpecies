using UnityEngine;

public class garden : MonoBehaviour
{
    public AudioClip compostSound;
    public AudioSource machine;

    private Vector3 initialPlantPosition = new Vector3(-16.8f, 0.6f, 6f);
    private float zOffsetPerPlant = 1f;

    public static int planted = 0;
    public static int column = 0;

    void Start()
    {
        planted = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NaturalPlant"))
        {
            machine.PlayOneShot(compostSound);

            Rigidbody plantRb = collision.gameObject.GetComponent<Rigidbody>();
            if (plantRb != null)
            {
                plantRb.isKinematic = true;
                plantRb.detectCollisions = false;
            }

            Vector3 newPosition = initialPlantPosition;

            if (planted == 4)
            {
                column = 0;
            }

            if (planted < 4)
            {
                newPosition.z += column * zOffsetPerPlant;
                newPosition.x = -16.8f;
            }
            else if (planted >= 4 && planted < 8)
            {
                newPosition.z += column * zOffsetPerPlant;
                newPosition.x = -16.1f;
            }
            else if (planted > 8)
            {
                Destroy(collision.gameObject);
            }
            

            collision.transform.position = newPosition;
            collision.transform.rotation = Quaternion.identity;
            collision.transform.SetParent(transform);

            Collider plantCollider = collision.gameObject.GetComponent<Collider>();
            if (plantCollider != null)
            {
                plantCollider.enabled = false;
            }

            column++;
            planted++;
        }
    }
}