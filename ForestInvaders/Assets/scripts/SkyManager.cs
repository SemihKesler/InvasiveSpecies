using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyManager : MonoBehaviour
{
    public Transform invasives;
    public Material skyboxMat;

    private int totalInvasive;
    private static int currInvasive;
    private static int setInvasive;

    public Color fogColor = Color.black;
    public float nightDensity = 0.1f;
    public float dayDensity = 0.00f;

    public float minExposure = 0.1f;
    public float maxExposure = 1.0f;
    public GameObject flashlight;

    public AudioClip nightSound;
    public AudioClip daySound;
    private AudioSource AudioSource;

    public float maxVolume = 0.5f;
    public float minVolume = 0.1f;

    private float transitionPoint = 0.4f;
    private bool nightPlaying = false;
    private bool dayPlaying = false;

    private bool exposureMap = true;
    private bool invasiveMapped = false;

    private float plantTotal = 3f;
    private static float planted = 0f;

    private bool failSafe = false;
    public bool forceDay = false;
    private bool lightOn = true;

    public void removeInvasive()
    {
        if (currInvasive > 0)
        {
            currInvasive--;
            updateSky();
        }
    }

    public void addPlant()
    {
        planted += 1;
        float sky = planted / plantTotal;
        Debug.Log("Plant Percent: " + sky + " | Exposure: " + Mathf.Lerp(minExposure, maxExposure, sky));
        if (planted >= 3)
        {
            forceDay = true;
        }
        updateSky();
    }


    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        RenderSettings.skybox = skyboxMat;
        totalInvasive = invasives.childCount;
        currInvasive = totalInvasive;
        setInvasive = totalInvasive;
        updateSky();
    }

    private void setWin()
    {
        RenderSettings.fogDensity = dayDensity;
        skyboxMat.SetFloat("_Exposure", maxExposure);
        setSound(true, maxVolume);
    }

    void setSound(bool day, float t)
    {
        if (forceDay)
        {
            day = true;
        }

        if (AudioSource != null)
        {
            if (day)
            {
                if (daySound != null && !dayPlaying)
                {
                    AudioSource.clip = daySound;
                    AudioSource.Play();
                    dayPlaying = true;
                }
            }
            else
            {
                if (nightSound != null && !nightPlaying)
                {
                    AudioSource.clip = nightSound;
                    AudioSource.Play();
                    nightPlaying = true;
                }
            }
            AudioSource.volume = Mathf.Lerp(minVolume, maxVolume, t);
        }
    }

    public void updateSky()
    {
        if (!failSafe)
        {
            float t = 1 - ((float)currInvasive / totalInvasive);
            float sky = planted / plantTotal;

            if (t >= transitionPoint)
            {
                float t2 = 1 - (Mathf.Abs((transitionPoint - t) * (1 / (1 - transitionPoint))));
                setSound(true, t2);

                if (flashlight != null)
                {
                    if (lightOn)
                    {
                        flashlight.SetActive(false);
                        lightOn = false;
                    }
                }
            }
            else
            {
                float t2 = (t * (1 / transitionPoint));
                setSound(false, t2);
            }
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = Mathf.Lerp(nightDensity, dayDensity, t);

            if (exposureMap)
            {
                if (invasiveMapped)
                {
                    skyboxMat.SetFloat("_Exposure", Mathf.Lerp(minExposure, maxExposure, t));
                }
                else
                {
                    skyboxMat.SetFloat("_Exposure", Mathf.Lerp(minExposure, maxExposure, sky));
                }
            }
            else
            {
                skyboxMat.SetFloat("_Exposure", minExposure);
            }
        }
        else
        {
            setWin();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) || CAVE2.GetButtonDown(CAVE2.Button.ButtonUp))
        {
            failSafe = !failSafe;
            if (!failSafe)
            {
                flashlight.SetActive(true);
                lightOn = true;
                nightPlaying = false;
                dayPlaying = false;
            }
            else
            {
                flashlight.SetActive(false);
                lightOn = false;
            }

            Debug.Log("Win State: " + failSafe);
            updateSky();
        }

        int newCount = invasives.childCount;
        if (setInvasive < newCount)
        {
            newCount = setInvasive;
        }
        if (newCount < setInvasive)
        {
            setInvasive = newCount;
        }

        if (newCount != currInvasive)
        {
            currInvasive = newCount;
            updateSky();
        }
    }
}
