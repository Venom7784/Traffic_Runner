using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpeed : MonoBehaviour
{
    [SerializeField] public float carspeed = 10f;
    public float Segspeed;

    [SerializeField] public  float speedIncreaseRate = 0.5f; // Units per second

    void Start()
    {
        Segspeed = carspeed - 10f;
    }

    void Update()
    {
        IncreaseSpeedOverTime();
    }

    void IncreaseSpeedOverTime()
    {
        carspeed += speedIncreaseRate * Time.deltaTime;
        Segspeed = carspeed - 10f;
    }
}
