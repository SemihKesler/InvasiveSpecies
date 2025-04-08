using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowInstruction : MonoBehaviour
{
    public TMP_Text instructionText;
    public float displayTime = 10f; // How long to show instructions

    void Start()
    {
        // Show the text when game starts
        instructionText.gameObject.SetActive(true);

        
        Invoke("HideInstruction", displayTime);
    }

    void HideInstruction()
    {
        // Hide the text after seconds passes
        instructionText.gameObject.SetActive(false);
    }
}
