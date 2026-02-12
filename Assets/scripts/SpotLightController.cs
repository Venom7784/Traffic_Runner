using UnityEngine;

public class SpotLightController : MonoBehaviour
{
    public Light[] spotlights; // Add your spotlights here
    public AnimationCurve intensityOverTime; // Curve from 0–1 timeOfDay

    void Update()
    {
        if (TimeOfDayManager.Instance == null) return;

        float time = TimeOfDayManager.Instance.timeOfDay;
        float normalizedTime = time / 24f;

        float intensity = intensityOverTime.Evaluate(normalizedTime);

        foreach (Light light in spotlights)
        {
            if (light != null)
            {
                light.intensity = intensity;
                light.enabled = intensity > 0.01f; // Optional: disable light if it's almost zero
            }
        }
    }
}
