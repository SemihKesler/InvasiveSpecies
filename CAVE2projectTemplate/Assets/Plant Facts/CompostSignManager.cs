using UnityEngine;
using UnityEngine.UI; // Or using TMPro if using TextMeshPro (ask me if you want TMP!)

public class CompostSignManager : MonoBehaviour
{
    public Text signText; // Link to the Text on your big sign

    // Call this to update the sign when composting a plant
    public void UpdateSign(string fact)
    {
        signText.text = fact;
    }
}
