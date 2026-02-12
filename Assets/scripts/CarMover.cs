using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{
    public static bool allowMovement = true;

    private CarSpeed carScript;  // 🔧 Declare it at class level so all methods can use it


    void Start()
    {
        carScript = GameObject.Find("CarSpeedAdjuster").GetComponent<CarSpeed>();

      


       
    }

    void Update()
    {
        MoveCar(carScript.carspeed);  // ✅ Now carScript is accessible here
    }

    void MoveCar(float moveSpeed)
    {
        if (!allowMovement) return;

        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);
    }

}
