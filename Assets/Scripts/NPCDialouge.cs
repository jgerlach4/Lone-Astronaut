using TMPro;
using UnityEngine;

public class NPCDialouge : MonoBehaviour
{
    public TMP_Text interactText; // Reference to the UI text element (set in Inspector)
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ensure the interact text is hidden at the start
        if (interactText != null)
        {
            interactText.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show interact text
            if (interactText != null)
            {
                interactText.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (interactText != null)
        {
            interactText.enabled = false;
        }
    }
}
