using UnityEngine;

public class SpinningBouncingCoin : MonoBehaviour
{
    public float rotationSpeed = 100f;   // Degrees per second
    public float bounceHeight = 0.5f;    // Max height offset from original
    public float bounceSpeed = 2f;       // How fast it bounces

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Spin the coin
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // Bounce the coin (up/down in Y using sine wave)
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
