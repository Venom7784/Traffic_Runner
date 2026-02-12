using UnityEngine;

public class SunRotation : MonoBehaviour
{
    void Update()
    {
        if (TimeOfDayManager.Instance == null) return;

        float timeOfDay = TimeOfDayManager.Instance.timeOfDay;

        // Convert timeOfDay (0–24) to angle (0–360)
        float sunAngle = (timeOfDay / 24f) * 360f;

        // Rotate the sun around the X axis
        transform.rotation = Quaternion.Euler(sunAngle - 90f, 170f, 0f);
        // -90 to make midnight at horizon, 170 Y to control direction
    }
}
