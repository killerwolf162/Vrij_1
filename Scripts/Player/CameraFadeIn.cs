using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraFadeIn : MonoBehaviour
{
    public float fade_duration = 1f;
    public Color fade_color = Color.black;
    public Image background;



    public void TriggerFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        fade_color.a = 1f; // Set initial transparency to fully opaque
        background.enabled = true; // Make sure the fade overlay is active

        // Fade in
        for (float t = 0; t < fade_duration; t += Time.deltaTime)
        {
            float normalizedTime = t / fade_duration;
            fade_color.a = Mathf.Lerp(1f, 0f, normalizedTime); // Fade from 1 to 0
            background.GetComponent<Image>().color = fade_color; // Apply the color to the overlay
            yield return null;
        }

        background.enabled = false; // Once the fade-in is complete, make the overlay inactive
    }
}