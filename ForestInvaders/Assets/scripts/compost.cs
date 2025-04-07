using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compost : MonoBehaviour
{
    public AudioClip compostSound;
    public AudioSource machine;
    public Text fact;
    public GameObject lid;
    public GameObject newPlantPrefab;
    public Image compostSign;
    public Sprite Compost_Image;
    public Sprite Johnsongrass_Image;
    public Transform spawnLocation;

    private int factIter = 0;
    private List<string> facts = new List<string>();
    [TextArea] public string fact1;
    [TextArea] public string fact2;
    [TextArea] public string fact3;
    bool blockColisions = false;

    
    void Start()
    {
        facts.Add(fact1);
        facts.Add(fact2);
        facts.Add(fact3);

        fact.text =
            @"About the Composting Process:" +
            "Compost";
            
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InvasivePlant"))
        {
            if (!blockColisions)
            {
                Destroy(collision.gameObject);
                PlantFact plant = collision.gameObject.GetComponent<PlantFact>();

                if (plant.fact == "Johnsongrass") {
                    compostSign.sprite = Johnsongrass_Image;
                }
                StartCoroutine(ResetScreen(10f));

                machine.PlayOneShot(compostSound);
                blockColisions = true;
                StartCoroutine(LidAnimation());

                StartCoroutine(SpawnNewObjectAfterDelay(1.5f));
            }
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
        blockColisions = false;

        if (newPlantPrefab != null && spawnLocation != null)
        {
            GameObject instance = null;
            if (instance == null)
            {
                instance = Instantiate(newPlantPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
                if (factIter > (facts.Count - 1))
                {
                    factIter = 0;
                }
                instance.GetComponent<PlantFact>().fact = facts[factIter];
                factIter++;
            }
        }
        else
        {
            Debug.LogWarning("Prefab or spawn location not set in the inspector!");
        }
    }

    IEnumerator ResetScreen(float delay) {
        yield return new WaitForSeconds(delay);
        compostSign.sprite = Compost_Image;
    }
}