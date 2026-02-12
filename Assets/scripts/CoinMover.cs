using UnityEngine;

public class CoinMover : MonoBehaviour
{
    public static bool allowMovement = true;

    private CarSpeed carScript;
    private Rigidbody rb;
    public float Magnitude;

    [Header("Magnet Settings")]
    public float magnetSpeed = 10f;
    public float magnetRadius = 8f;

    void Start()
    {
        carScript = GameObject.Find("CarSpeedAdjuster").GetComponent<CarSpeed>();
        rb = GetComponent<Rigidbody>();
       
    }

    void FixedUpdate()
    {
        if (rb == null) return;
        if(allowMovement)
        {
            Magnitude = carScript.carspeed;
        }
        else
        {
            Magnitude = 0f;
        }

        // Base movement (e.g., endless runner)
        Vector3 velocity = Vector3.back * Magnitude;

        // Add magnet attraction if within radius
        if (CoinCollector.isMagnetic)
        {
            Vector3 playerPos = CoinCollector.playerPosition;
            float distance = Vector3.Distance(transform.position, playerPos);

            if (distance <= magnetRadius)
            {
                Vector3 direction = (playerPos - transform.position).normalized;
                velocity += direction *(Magnitude*magnetSpeed );
            }
        }

        rb.velocity = velocity;
    }
}
