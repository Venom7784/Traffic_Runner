using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public GameObject[] segmentPrefabs;
    public int initialSegments = 5;
    public float spawnThresholdZ = 500f;

    private Queue<GameObject> spawnedSegments = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < initialSegments; i++)
        {
            SpawnNextSegment();
        }
    }

    void Update()
    {
        if (spawnedSegments.Count > 0)
        {
            float lastZMax = GetSegmentZMax(spawnedSegments.Last());

            if (lastZMax < spawnThresholdZ)
            {
                SpawnNextSegment();
            }
        }
    }

    float GetSegmentZMax(GameObject segment)
    {
        BoxCollider box = segment.GetComponent<BoxCollider>();
        if (box == null) return segment.transform.position.z;

        Vector3 worldCenter = segment.transform.TransformPoint(box.center);
        Vector3 worldSize = Vector3.Scale(box.size, segment.transform.lossyScale);

        return worldCenter.z + worldSize.z / 2f;
    }

    float GetSegmentHalfZSize(GameObject prefab)
    {
        BoxCollider box = prefab.GetComponent<BoxCollider>();
        if (box == null) return 0f;

        Vector3 scaledSize = Vector3.Scale(box.size, prefab.transform.localScale);
        return scaledSize.z / 2f;
    }

    void SpawnNextSegment()
    {
        GameObject prefab = segmentPrefabs[Random.Range(0, segmentPrefabs.Length)];

        // Determine Z-back of last segment
        float lastZMax = 0f;
        if (spawnedSegments.Count > 0)
        {
            lastZMax = GetSegmentZMax(spawnedSegments.Last());
        }

        // Get half Z-size of new segment to shift it forward
        float newHalfZ = GetSegmentHalfZSize(prefab);

        Vector3 spawnPos = new Vector3(0, 0, lastZMax + newHalfZ+16);
        GameObject newSegment = Instantiate(prefab, spawnPos, Quaternion.identity);
        spawnedSegments.Enqueue(newSegment);
    }
}
