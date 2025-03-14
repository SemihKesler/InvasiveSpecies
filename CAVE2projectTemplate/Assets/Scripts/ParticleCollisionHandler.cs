using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    public string targetTag = "Invasive"; // Tag of objects that can fade
    public float fadeSpeed = 4.0f; // Speed at which the object fades
    public AudioClip collisionSound; // Sound to play on collision

    private void OnParticleCollision(GameObject other)
    {
        // Check if the collided object has the specified tag
        if (other.CompareTag(targetTag))
        {
     
            // Play the collision sound at the position of the collision
            if (collisionSound != null)
            {
                AudioSource.PlayClipAtPoint(collisionSound, other.transform.position);
            }

            // Start fading the object
            StartCoroutine(FadeObject(other));

        }
    }

    private System.Collections.IEnumerator FadeObject(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = renderer.material;
            Color initialColor = material.color;

            // Gradually reduce the alpha value
            while (material.color.a > 0)
            {
                Color newColor = material.color;
                newColor.a -= fadeSpeed * Time.deltaTime; // Reduce alpha over time
                material.color = newColor;
                yield return null; // Wait for the next frame
            }

            // Optionally destroy or disable the object after fading
            Destroy(obj); // Destroy the object
            // OR
            // obj.SetActive(false); // Disable the object
        }
    }
}