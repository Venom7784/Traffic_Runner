using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public static bool isMagnetic = false;
    public static Vector3 playerPosition; // Accessible to all coins

    public int coinCount = 0;

    // Magnetic effect duration
    public float magnetDuration = 5f;
    private float magnetTimer = 0f;

    public AudioClip CoinCollectFX;

    void Update()
    {
        if (isMagnetic)
        {
            // Update the position for coins to attract to
            playerPosition = transform.Find("MagnetAttractor").position;

            // Count down the timer
            magnetTimer -= Time.deltaTime;
            if (magnetTimer <= 0f)
            {
                isMagnetic = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(other.gameObject);
            SoundFXManager.Instance.PlaySoundClip(CoinCollectFX, transform, 1f);

        }

        if (other.CompareTag("Magnet"))
        {
            isMagnetic = true;
            magnetTimer = magnetDuration; // Reset the timer
            Destroy(other.gameObject); // Optional
        }
    }

    public int GiveInfoCoin()
    {
       return coinCount;
    }
}
