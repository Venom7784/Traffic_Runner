using UnityEngine;

public class SegmentMover : MonoBehaviour
{
    public static bool allowMovement = true;

    private CarSpeed carScript;  // 🔧 Declare it at class level so all methods can use it

    void Start()
    {
        carScript = GameObject.Find("CarSpeedAdjuster").GetComponent<CarSpeed>();
    }

    void Update()
    {
        MoveCar(carScript.Segspeed);  // ✅ Now carScript is accessible here
    }

    void MoveCar(float moveSpeed)
    {
        if (!allowMovement) return;

        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);
    }
}
