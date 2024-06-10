using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EagleVisionController : MonoBehaviour
{
    public Material eagleVisionMaterial;
    public Volume normalPostProcessVolume;  // Reference to the normal post-process volume
    public Volume eagleVisionPostProcessVolume;  // Reference to the eagle vision post-process volume

    private Material[] originalMaterials;
    private Renderer[] renderers;

    void Start()
    {
        // Store the original materials of this GameObject and its children
        renderers = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].material;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetEagleVision(true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SetEagleVision(false);
        }
    }

    void SetEagleVision(bool activate)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = activate ? eagleVisionMaterial : originalMaterials[i];
        }

        if (normalPostProcessVolume != null)
        {
            normalPostProcessVolume.weight = activate ? 0 : 1;
        }

        if (eagleVisionPostProcessVolume != null)
        {
            eagleVisionPostProcessVolume.weight = activate ? 1 : 0;
        }
    }
}
