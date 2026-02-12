using UnityEngine;

public class AtmospherController : MonoBehaviour
{
    [Header("Ambient Light")]
    public Gradient ambientColorOverTime;

    [Header("Fog Settings")]
    public Gradient fogColorOverTime;
    public AnimationCurve fogDensityOverTime;

    public Gradient skyTintOverTime;

    void Update()
    {
        if (TimeOfDayManager.Instance == null) return;

        float time = TimeOfDayManager.Instance.timeOfDay;
        float normalizedTime = time / 24f;

        // Update ambient light
        RenderSettings.ambientLight = ambientColorOverTime.Evaluate(normalizedTime);

        // Update fog color
        RenderSettings.fogColor = fogColorOverTime.Evaluate(normalizedTime);

        // Update fog density
        RenderSettings.fogDensity = fogDensityOverTime.Evaluate(normalizedTime);

        RenderSettings.skybox.SetColor("_SkyTint", skyTintOverTime.Evaluate(normalizedTime));
        RenderSettings.skybox.SetFloat("_TimeOfDay", normalizedTime);
        DynamicGI.UpdateEnvironment();
    }
}
