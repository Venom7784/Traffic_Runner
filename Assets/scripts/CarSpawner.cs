using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class CarSpawner : MonoBehaviour
{

    public GameObject[] carPrefabs; // different car models
    public List<int> FilledLanes = new List<int>();
    List<int> CanSpwan = new List<int>() {0};
    private CarSpeed carScript;

    public CoinSpawner coinSpawner;

    public Transform player;
    public float SpawnDistance = 100;
    public float InterCarspawnDistance = 15;
    public float carMoveSpeed = 5f;
    public int count = 0;
    public float spawnInterval => InterCarspawnDistance / carScript.carspeed;

    public float InterCoinSetDistance = 10;
    private float timer = 0f;
    private float[] lanes = new float[] { 21f, 26.77f, 31f }; // X positions for lanes
    void Start()
    {
        carScript = GameObject.Find("CarSpeedAdjuster").GetComponent<CarSpeed>();

    }
   
    void Update()
    {
      
        timer += Time.deltaTime;
       
        if (timer >= spawnInterval)
        {
          
            SpawnCars();
            CheckLanes();
            timer = 0f;
        }
       
    }

    void SpawnCars()
    {
        // Randomly choose which 1 or 2 lanes will have cars
        for (int i = 0; i <3; i++)
        {
            if (CanSpwan.Contains(i)  && Random.value > 0.5f ) // Optional: 30% chance to not fill even if allowed
            {
                FilledLanes.Add(i);
                Vector3 spawnPos = new Vector3(lanes[i], 0f, player.position.z+SpawnDistance);
                GameObject car = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], spawnPos, Quaternion.Euler(0f, 180f, 0f));
            }
            if (Random.value > 0.5f  && !CanSpwan.Contains(i) )
            {

                coinSpawner.SpawnCoinInEmptyLanes(i);
            }
        }
    }
    void CheckLanes()
    {
        CanSpwan.Clear();
        if (FilledLanes.Count == 0 )
        {
            if(Random.value<0.3f)
            CanSpwan = new List<int>() { 0 ,1};
            else if(Random.value<0.6)
                CanSpwan = new List<int>() { 0, 2};
            else
                CanSpwan = new List<int>() { 1, 2 };
        }
        else if (FilledLanes.Count == 2)
        {
            CanSpwan = FilledLanes;
        }
        else 
        {
            if (FilledLanes[0] == 1)
            {
                CanSpwan.Add(1);
            }
            else if (FilledLanes[0] == 2)
            {

                CanSpwan=new List<int> { UnityEngine.Random.Range(0, 3) };
               
            }
            else if (FilledLanes[0] == 0 )
            {
                CanSpwan = new List<int> { UnityEngine.Random.Range(0, 3) };
            }
        }
        FilledLanes.Clear();
    }
    public List<int> GenerateList(int input)
    {
        int length = UnityEngine.Random.Range(1, 3); // Generates 0, 1, or 2
        
        if (length == 1)
        {
            return new List<int> { UnityEngine.Random.Range(0, 3) }; // 0, 1, or 2
        }
        else
        {
            int first = UnityEngine.Random.Range(0, 3);
            int second;
            if (input == 0)
            {
                // Ensure 1 and 2 are not together
                if (first == 1)
                {
                    second = UnityEngine.Random.Range(0, 2); // 0 or 1
                }
                else if (first == 2)
                {
                    second = UnityEngine.Random.Range(0, 2) == 0 ? 0 : 2; // 0 or 2
                }
                else // first == 0
                {
                    second = UnityEngine.Random.Range(0, 3); // 0, 1, or 2
                }
            }
            else if (input == 2)
            {
                // Ensure 0 and 1 are not together
                if (first == 0)
                {
                    second = UnityEngine.Random.Range(0, 2) == 0 ? 0 : 2; // 0 or 2
                }
                else if (first == 1)
                {
                    second = UnityEngine.Random.Range(1, 3); // 1 or 2
                }
                else // first == 2
                {
                    second = UnityEngine.Random.Range(0, 3); // 0, 1, or 2
                }
            }
            else
            {
                // Default behavior: ensure 1 and 2 are not together
                if (first == 0)
                {
                    second = UnityEngine.Random.Range(0, 3); // 0, 1, or 2
                }
                else if (first == 1)
                {
                    second = UnityEngine.Random.Range(0, 2); // 0 or 1
                }
                else // first == 2
                {
                    second = UnityEngine.Random.Range(0, 2) == 0 ? 0 : 2; // 0 or 2
                }
            }
            return new List<int> { first, second };
        }
    }
}


