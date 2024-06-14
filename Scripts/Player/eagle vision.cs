using UnityEngine;
using System.Collections.Generic;

public class EagleVisionMultiple : MonoBehaviour
{
    public Material eagleVisionMaterial; // Assign this in the inspector
    private Dictionary<Renderer, Material[]> originalMaterials;
    private bool isEagleVisionEnabled = false;
    private Camera normalCamera, smellCamera;

    void Start()
    {

        normalCamera = GameObject.FindGameObjectWithTag("Player_camera").GetComponent<Camera>();
        normalCamera = GameObject.FindGameObjectWithTag("Smell_camera").GetComponent<Camera>();

        // Find all renderers in the scene
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        originalMaterials = new Dictionary<Renderer, Material[]>();

        // Store the original materials for each renderer
        foreach (Renderer renderer in renderers)
        {
            originalMaterials[renderer] = renderer.sharedMaterials;
        }
    }

    void Update()
    {
        // Toggle Eagle Vision on spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleEagleVision();
            normalCamera.enabled = false;
            smellCamera.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ToggleEagleVision();
            normalCamera.enabled = true;
            smellCamera.enabled = false;
        }
    }

    void ToggleEagleVision()
    {
        isEagleVisionEnabled = !isEagleVisionEnabled;

        foreach (Renderer renderer in originalMaterials.Keys)
        {
            if (isEagleVisionEnabled)
            {
                // Apply Eagle Vision material
                Material[] eagleVisionMaterials = new Material[renderer.sharedMaterials.Length];
                for (int i = 0; i < eagleVisionMaterials.Length; i++)
                {
                    eagleVisionMaterials[i] = eagleVisionMaterial;
                }
                renderer.materials = eagleVisionMaterials;
            }
            else
            {
                // Restore original materials
                renderer.materials = originalMaterials[renderer];
            }
        }
    }
}

