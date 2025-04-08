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

    public Image gardenSign;
    public Sprite Garden_Image;
    public Sprite Milkweed_Image;
    public Sprite PurpleConeflower_Image;
    public Sprite DooryardViolet_Image;

    void Start()
    {
        planted = 0;
        gardenSign.sprite = Garden_Image;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NaturalPlant"))
        {
            machine.PlayOneShot(compostSound);
            totalPlots--;
            PlantFact plant = collision.gameObject.GetComponent<PlantFact>();
            // string screen = @"Garden: "+totalPlots+ "/6 Spaces Remaining\n" +
            //     "\nValid Plants:" +
            //     "\n- Milkweed" +
            //     "\n- Dooryard Violet" +
            //     "\n- PurpleCornflower\n\n" + plant.fact;

            if (plant.fact == "Milkweed") {
                gardenSign.sprite = Milkweed_Image;
            }
            else if (plant.fact == "Purple Coneflower") {
                gardenSign.sprite = PurpleConeflower_Image;
            }
            else if (plant.fact == "Dooryard Violet") {
                gardenSign.sprite = DooryardViolet_Image;
            }
            
            // gardenScreen.text = screen;

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
                newPosition.x = -18.8f + (column * xOffsetPerPlant);
                newPosition.z = initialPlantPosition.z + (column * zOffsetPerPlant);
            }
            else if (planted >= 4 && planted < 8)
            {
                newPosition.x = -18.1f + (column * xOffsetPerPlant);
                newPosition.z = initialPlantPosition.z + (column * zOffsetPerPlant);
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