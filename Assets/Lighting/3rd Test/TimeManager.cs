using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TimeSettings timeSettings;
    TimeService service;

    [Header("Lights used")]
    [SerializeField] Light dayLight;
    [SerializeField] Light nightLight;

    [Header("Script Variables")]
    [SerializeField] float maxDayLightIntensity = 1f;
    [SerializeField] float maxNightLightIntensity = 0.5f;
    [SerializeField] AnimationCurve lightIntensityCurve;
    [SerializeField] Color dayAmbientLight;
    [SerializeField] Color nightAmbientLight;
    [SerializeField] Volume volume;
    ColorAdjustments colorAdjustments;

    private void Start()
    {
        service = new TimeService(timeSettings);
        volume.profile.TryGet(out colorAdjustments);
    }

    private void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
    }

    void UpdateLightSettings()
    {
        bool isNight = !service.IsDayTime();
        nightLight.enabled = isNight;
        dayLight.enabled = !isNight;

        float dotProduct = Vector3.Dot(dayLight.transform.forward, Vector3.down);
        
        dayLight.intensity = Mathf.Lerp(0, maxDayLightIntensity, lightIntensityCurve.Evaluate(dotProduct));
        nightLight.intensity = Mathf.Lerp(0, maxNightLightIntensity, lightIntensityCurve.Evaluate(1 - dotProduct));

        if (colorAdjustments != null) 
        { 
            colorAdjustments.colorFilter.value = Color.Lerp(nightAmbientLight, dayAmbientLight, lightIntensityCurve.Evaluate(dotProduct)); 
        }
        else { return; }
        
        
    }

    void RotateSun()
    {
        float rotation = service.CalculateSunAngle();
        dayLight.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.right);
    }

    void UpdateTimeOfDay()
    {
        service.UpdateTime(Time.deltaTime);

        if(timeText != null)
        {
            timeText.text = service.CurrentTime.ToString("HH:mm");
        }
    }
}
