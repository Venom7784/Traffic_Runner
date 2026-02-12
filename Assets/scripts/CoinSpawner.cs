using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    public GameObject[] PowerUps;
    public GameObject coinPrefab;
    public Transform player;
    public float probOfMagnets=0.99f;
    public float coinSpawnDistance = 100f;
    private float[] lanes = new float[] { 21f, 26.77f, 31f };
    public void SpawnCoinInEmptyLanes(int emptyLaneIndex)
    {
        if (Random.value >probOfMagnets)
        {
            for (float i = 0f; i < 6.67; i += 3.33f)
            {

                Vector3 spawnPos = new Vector3(lanes[emptyLaneIndex], 1f, player.position.z + coinSpawnDistance -5+ i);

                Instantiate(coinPrefab, spawnPos, Quaternion.identity);
            }
        }
        else
        {
            Vector3 spawnPos = new Vector3(lanes[emptyLaneIndex], 1f, player.position.z + coinSpawnDistance);
            Instantiate(PowerUps[Random.Range(0,PowerUps.Length)], spawnPos, Quaternion.identity);
        }

    }


}
