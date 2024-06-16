using UnityEngine;
using TMPro; // Add this namespace for TextMesh Pro
using UnityEngine.UI;
using System.Collections;

public class DisplayText : MonoBehaviour
{
    public TextMeshProUGUI characterText; // Reference to the TextMeshProUGUI component
    public GameObject panel; // Reference to the Panel object
    public string message = "Hello, welcome to the game!"; // Message to display
    public float displayDelay = 2f; // Delay before showing the text
    public float displayDuration = 5f; // Duration to show the text

    private void Awake()
    {
        characterText.enabled = false;
        panel.SetActive(false);
    }
    void Start()
    {
        // Start the coroutine to display the text and panel
        StartCoroutine(ShowTextCoroutine());
    }

    IEnumerator ShowTextCoroutine()
    {
        // Wait for the initial delay
        yield return new WaitForSeconds(displayDelay);

        // Show the text and panel
        if (characterText != null && panel != null)
        {
            characterText.text = message;
            panel.SetActive(true);
            characterText.enabled = true;        
        }
        else
        {
            Debug.LogError("TextMeshProUGUI or Panel component is not assigned.");
        }

        // Wait for the display duration
        yield return new WaitForSeconds(displayDuration);
        characterText.enabled = false;
        panel.SetActive(false);
        StopCoroutine(ShowTextCoroutine());
    }
}