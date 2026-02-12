using UnityEngine;

[RequireComponent(typeof(Light))]
public class SunController : MonoBehaviour
{
    public Gradient sunColor;                    // Color over time
    public AnimationCurve sunIntensityCurve;     // Intensity over time

    private Light sunLight;


    void Start()
    {
        sunLight = GetComponent<Light>();
    }

    void Update()
    {
        if (TimeOfDayManager.Instance == null) return;

        float timeOfDay = TimeOfDayManager.Instance.timeOfDay;

        // Normalize time to [0,1]
        float normalizedTime = timeOfDay / 24f;

        // Rotate the sun
        float sunAngle = normalizedTime * 360f;
        transform.rotation = Quaternion.Euler(sunAngle - 90f, 0f, 0f); // adjust for direction

        // Set color and intensity from curves
        sunLight.color = sunColor.Evaluate(normalizedTime);
        sunLight.intensity = sunIntensityCurve.Evaluate(normalizedTime);

     
    }
}
