using UnityEngine;
using UnityEngine.UI; // Make sure to include this for UI elements

public class FactPopupManager : MonoBehaviour
{
    public Text popupText; // Reference to the Text component (inside your Canvas)

    // Call this method when you want to show a fact
    public void ShowFact(string fact)
    {
        popupText.text = fact; // Set the text message
        popupText.gameObject.SetActive(true); // Show the text box
        CancelInvoke(); // Stop any previous hide calls (so timer resets)
        Invoke("HideFact", 4f); // Automatically hide after 4 seconds
    }

    // Method to hide the popup text
    void HideFact()
    {
        popupText.gameObject.SetActive(false); // Hide the text box
    }
}
