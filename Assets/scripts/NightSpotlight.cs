using UnityEngine;

[RequireComponent(typeof(Light))]
public class NightSpotlight : MonoBehaviour
{
    public AnimationCurve intensityOverTime;
    public Gradient colorOverTime; // Optional: if you want the light color to change
    public float maxIntensity = 1f;

    private Light spot;

    void Awake()
    {
        spot = GetComponent<Light>();
    }

    void Update()
    {
        if (TimeOfDayManager.Instance == null) return;

        float normalizedTime = TimeOfDayManager.Instance.timeOfDay / 24f;
        float curveValue = intensityOverTime.Evaluate(normalizedTime);

        // Set intensity and optionally color
        spot.intensity = curveValue * maxIntensity;
        spot.enabled = curveValue > 0.01f;

        if (colorOverTime.colorKeys.Length > 0)
            spot.color = colorOverTime.Evaluate(normalizedTime);
    }
}
