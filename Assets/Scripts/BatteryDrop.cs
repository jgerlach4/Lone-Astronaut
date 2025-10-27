using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WeaponDrop : MonoBehaviour
{
    private GameObject itemInRange;
    public TMP_Text interactText; // Reference to the UI text element (set in Inspector)
    public GameObject heldBattery; // The weapon that disappears after dropoff (set in Inspector)
    public GameObject worldBattery; // The weapon in the world (set in Inspector)
    // public extern Inventory weaponSubtract(GameObject weapon);

    private void Start()
    {
        // Ensure the interact text is hidden at the start
        if (interactText != null)
        {
            interactText.enabled = false;
        }
    }
    // Called automatically when another collider enters this trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && heldBattery.activeSelf)
        {
            // Show interact text
            if (interactText != null)
            {
                interactText.enabled = true;
            }
            itemInRange = this.gameObject;
        }
    }
    // Optional: when object stays inside trigger
    private void OnTriggerStay(Collider other)
    {
        if (!heldBattery.activeSelf) return; // Prevent dropping if no weapon is held
        Debug.Log("In Drop Zone");
        // Runs every frame while inside
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Dropped Battery");
            // Set held weapon to inactive
            if (heldBattery != null)
            {
                heldBattery.SetActive(false);
            }
            if (worldBattery != null)
            {
                worldBattery.SetActive(true);
                SceneManager.LoadScene(2);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (interactText != null)
            {
                interactText.enabled = false;
            }
        }
    }
    // Optional: when object exits trigger
    private void OnTriggerExit(Collider other)
    {
        if (interactText != null)
        {
            interactText.enabled = false;
        }
    }
}