using UnityEngine;

public class SegmentDespawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Segment") || other.CompareTag("obstacle") || other.CompareTag("Coin")) 
        {
            Destroy(other.gameObject);
        }
    }
}
