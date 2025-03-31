using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class compost : MonoBehaviour
{
    public AudioClip compostSound;
    public AudioSource machine;
    public Text fact;
    public GameObject lid;
    public GameObject newPlantPrefab; // Reference to the prefab you want to spawn
    public Transform spawnLocation; // Where the new object should appear

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

            StartCoroutine(LidAnimation());

            // Spawn a new object after a delay (optional)
            StartCoroutine(SpawnNewObjectAfterDelay(1.5f)); // Adjust delay as needed
        }
    }

    IEnumerator LidAnimation()
    {
        Quaternion startRotation = lid.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));

        float time = 0;
        while (time < 1)
        {
            lid.transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            time += Time.deltaTime;
            yield return null;
        }
        lid.transform.rotation = endRotation;

        yield return new WaitForSeconds(1f);

        time = 0;
        startRotation = lid.transform.rotation;
        endRotation = Quaternion.Euler(Vector3.zero);

        while (time < 1)
        {
            lid.transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            time += Time.deltaTime;
            yield return null;
        }
        lid.transform.rotation = endRotation;
    }

    IEnumerator SpawnNewObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (newPlantPrefab != null && spawnLocation != null)
        {
            Instantiate(newPlantPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
        }
        else
        {
            Debug.LogWarning("Prefab or spawn location not set in the inspector!");
        }
    }
}