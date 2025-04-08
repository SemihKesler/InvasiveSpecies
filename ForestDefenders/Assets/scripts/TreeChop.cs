using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChop : MonoBehaviour
{
    private GameObject tree;
    private GameObject stump;
    private bool chopped = false;
    private AudioSource audioSource;
    [SerializeField] private AudioClip chop;
    private Renderer renderer;
    private Material material;
    [SerializeField] private float fadeSpeed = 0.025f;
    private bool destroy = false;
    public SkyManager skyManager;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        tree = gameObject.transform.GetChild(0).gameObject;
        stump = gameObject.transform.GetChild(1).gameObject;
        if (tree != null && stump != null)
        {
            Collider treeCollider = tree.GetComponent<Collider>();
            Collider stumpCollider = stump.GetComponent<Collider>();
            if (treeCollider != null && stumpCollider != null)
            {
                Collider host = GetComponent<Collider>();
                Physics.IgnoreCollision(host, treeCollider);
                Physics.IgnoreCollision(host, stumpCollider);
            }
            renderer = stump.GetComponent<Renderer>();
            material = renderer.material;
        }
        else
        {
            Debug.Log("Missing Tree Or Log");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Tree Collide");
        if (collision.gameObject.CompareTag("Axe") && !chopped)
        {
            if (tree == null || stump == null)
            {
                Debug.Log("Missing Tree or Stump");
                return;
            }

            Rigidbody treeRb = tree.GetComponent<Rigidbody>();
            if (treeRb != null)
            {
                treeRb.velocity = Vector3.zero;
                treeRb.constraints = RigidbodyConstraints.None;
                if (audioSource != null)
                {
                    if (chop != null)
                    {
                        audioSource.PlayOneShot(chop);
                    }
                }
                Destroy(tree, 5f);
                chopped = true;
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (chopped)
        {
            Color newColor = material.color;
            newColor.r -= fadeSpeed;
            newColor.g -= fadeSpeed;
            material.color = newColor;

            if (material.color.r <= 0f && material.color.g <= 0f)
            {
                if (skyManager != null)
                {
                    skyManager.removeInvasive();
                }
                Destroy(gameObject, 1f);
            }
        }
    }
}
