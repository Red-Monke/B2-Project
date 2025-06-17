using UnityEngine;

public class EmissionControl : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Color nightEmissionColor;
    [SerializeField] private float emissionIntensity;

    private Material material;
    private TimeManager timeManager;

    private void Start()
    {
        material = targetRenderer.material;
        timeManager = TimeManager.Instance; // Access persistent time system
    }

    private void Update()
    {
        bool isNight = !timeManager.service.IsDayTime();

        if (isNight)
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", nightEmissionColor * emissionIntensity);
        }
        else
        {
            material.DisableKeyword("_EMISSION"); // Fully disables emission during the day
        }
    }
}
