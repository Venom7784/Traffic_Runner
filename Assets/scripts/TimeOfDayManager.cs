using UnityEngine;

public class TimeOfDayManager : MonoBehaviour
{
    [Range(0f, 24f)]
    public float timeOfDay = 12f; // 0 to 24
    public float timeSpeed = 1f;  // Speed of time progression

    public static TimeOfDayManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
      
        timeOfDay += Time.deltaTime * timeSpeed;
        if (timeOfDay >= 24f)
            timeOfDay = 0f;
    }

    public bool IsNight()
    {
        return timeOfDay < 6f || timeOfDay >= 18f; // Night: 6 PM to 6 AM
    }
}
