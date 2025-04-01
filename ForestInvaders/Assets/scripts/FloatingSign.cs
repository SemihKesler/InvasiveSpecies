using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSign : MonoBehaviour
{
    public float floatSpeed = 1f; // Speed for it floating
    public float floatHeight = 0.5f; // How high it moves up and down

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Make the sign float up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
