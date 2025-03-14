using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    public string targetTag = "Invasive"; // Tag of objects that can fade
    public float fadeSpeed = 0.05f; // Speed at which the object fades
    public AudioClip collisionSound; // Sound to play on collision
    private int hits = 0;
    private int prevHit = -1;
    private Renderer renderer;
    private Material material;

    private Color newColor;


    void Start()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material;
    }
    private void OnParticleCollision(GameObject other)
    {
        // Check if the collided object has the specified tag
        if (gameObject.CompareTag(targetTag))
        {
            // Play the collision sound at the position of the collision
            if (collisionSound != null)
            {
                AudioSource.PlayClipAtPoint(collisionSound, other.transform.position);
            }
            Color newColor = material.color;
            newColor.a -= 0.05f;
            Debug.Log(newColor.a);
            material.color = newColor;
            if (material.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}