using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; // or 30 for lower-end devices
        QualitySettings.vSyncCount = 1; // Use 0 to disable, 1 to match screen refresh rate

    }

    
}
