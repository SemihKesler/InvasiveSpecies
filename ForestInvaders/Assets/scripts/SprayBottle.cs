using UnityEngine;

public class SprayBottle : MonoBehaviour
{
    public ParticleSystem sprayParticles;
    bool changeState = false;
    private AudioSource audio;
    private GrabbableObject grabbableObject;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        Transform Parent = transform.parent;
        grabbableObject = Parent.GetComponent<GrabbableObject>();

        if (sprayParticles == null)
        {
            Debug.LogError("SprayParticles is not assigned in the Inspector!");
        }
        else
        {
            Debug.Log("SprayParticles is assigned correctly.");
        }
    }

    void Update()
    {
        if (grabbableObject != null)
        {
            if (grabbableObject.getGrabbed())
            {
                Audio(true);
                Spray(true);
            }
            else
            {
                Audio(false);
                Spray(false);
            }
        }
        else
        {
            Audio(true);
            Spray(true);
        }
    }

    void Spray(bool toggle)
    {
        if (toggle)
        {
            if (!sprayParticles.isPlaying)
            {
                sprayParticles.Play();
            }
        }
        else
        {
            if (sprayParticles.isPlaying)
            {
                sprayParticles.Stop();
            }
        }
    }


    void Audio(bool toggle)
    {
        if (audio != null)
        {
            if (toggle)
            {
                if (!audio.isPlaying)
                {
                    audio.Play();
                }
            }
            else
            {
                if (audio.isPlaying)
                {
                    audio.Stop();
                }
            }

        }
    }
}