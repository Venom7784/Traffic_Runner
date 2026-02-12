using UnityEngine;

public class ShadowManager : MonoBehaviour
{
    [System.Serializable]
    public class ShadowPair
    {
        public Transform target;      // The object to follow
        public Transform shadowQuad;  // The shadow object (quad)
        public float yOffset = 0.01f; // Optional offset from ground
    }

    public ShadowPair[] shadows;

    void Update()
    {
        foreach (var pair in shadows)
        {
            if (pair.target != null && pair.shadowQuad != null)
            {
                Vector3 pos = pair.target.position;
                pair.shadowQuad.position = new Vector3(pos.x, 0 + pair.yOffset, pos.z);
            }
        }
    }
}
