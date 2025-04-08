using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class garden : MonoBehaviour
{
    public AudioClip compostSound;
    public AudioSource machine;

    private Vector3 initialPlantPosition = new Vector3(-18.8f, 0.6f, 6f);
    private float zOffsetPerPlant = 0.75f;
    private float xOffsetPerPlant = 0.75f;

    public static int planted = 0;
    public static int column = 0;

    public Text gardenScreen;
    private int totalPlots = 6;
    public SkyManager skyManager;

    void Start()
    {
        planted = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NaturalPlant"))
        {
            machine.PlayOneShot(compostSound);
            totalPlots--;
            PlantFact plant = collision.gameObject.GetComponent<PlantFact>();
            string screen = @"Garden: "+totalPlots+ "/6 Spaces Remaining\n" +
                "\nValid Plants:" +
                "\n- Milkweed" +
                "\n- Dooryard Violet" +
                "\n- PurpleCornflower\n\n" + plant.fact;
            
            gardenScreen.text = screen;

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

            // Adjust the planting positions based on the rotated garden
            if (planted < 4)
            {
                // Diagonal adjustment: Using both x and z offsets for diagonal planting
                newPosition.x = -18.8f + (column * xOffsetPerPlant);  // Adjust x for diagonal planting
                newPosition.z = initialPlantPosition.z + (column * zOffsetPerPlant);  // Adjust z for diagonal planting
            }
            else if (planted >= 4 && planted < 8)
            {
                // Continue with the diagonal planting in this section
                newPosition.x = -18.1f + (column * xOffsetPerPlant);  // Adjust x for diagonal planting
                newPosition.z = initialPlantPosition.z + (column * zOffsetPerPlant);  // Adjust z for diagonal planting
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
            if (skyManager != null)
            {
                skyManager.addPlant();
            }
        }
    }
}