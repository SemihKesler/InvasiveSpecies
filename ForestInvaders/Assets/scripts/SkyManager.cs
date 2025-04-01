using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class SkyManager : MonoBehaviour
{
    public Transform invasives;
    public Material skyboxMat;

    private int totalInvasive;
    private int currInvasive;

    public Color night = Color.black;
    public Color day = Color.white;
    public float nightDensity = 0.1f;
    public float dayDensity = 0.01f;

    public float minExposure = 0.1f;
    public float maxExposure = 1.0f;
    public GameObject flashlight;

    public AudioClip nightSound;
    public AudioClip daySound;
    private AudioSource AudioSource;

    public float maxVolume = 0.5f;
    public float minVolume = 0.1f;

    private bool nightPlaying = false;
    private bool dayPlaying = false;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        RenderSettings.skybox = skyboxMat;
        totalInvasive = invasives.childCount;
        currInvasive = totalInvasive;
        updateSky();
    }

    void updateSky()
    {
        float t = 1 - ((float)currInvasive / totalInvasive);

        if (t >= 0.5)
        {
            if (daySound != null)
            {
                if (!dayPlaying)
                {
                    AudioSource.clip = daySound;
                    AudioSource.Play();
                    dayPlaying = true;
                }
                float t2 = 1 - (Mathf.Abs((0.5f - t) * 2f));
                AudioSource.volume = Mathf.Lerp(minVolume, maxVolume, t2);
            }
            if (flashlight != null)
            {
                flashlight.SetActive(false);
            }
        }
        else
        {
            if (nightSound != null)
            {
                if (!nightPlaying)
                {
                    AudioSource.clip = nightSound;
                    AudioSource.Play();
                    nightPlaying = true;
                }
                float t2 = 1 - (t * 2f);
                AudioSource.volume = Mathf.Lerp(maxVolume, minVolume, t2);
            }
        }

        RenderSettings.fogColor = Color.Lerp(night, day, t);
        RenderSettings.fogDensity = Mathf.Lerp(nightDensity, dayDensity, t);
        skyboxMat.SetFloat("_Exposure", Mathf.Lerp(minExposure, maxExposure, t));
    }
    void Update()
    {
        int newCount = invasives.childCount;
        if (newCount != currInvasive)
        {
            currInvasive = newCount;
            updateSky();
        }
    }
}
