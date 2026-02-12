using UnityEngine;

public class WIndowGlow : MonoBehaviour
{
    public Texture dayTexture;
    public Texture nightTexture;
    public Texture emissionMap;

    private Material material;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogError("No Renderer found on " + gameObject.name);
            return;
        }

        material = rend.material; // Get a unique instance of the material
        UpdateTexture();
    }

    void Update()
    {
        UpdateTexture();
    }

    void UpdateTexture()
    {
        if (TimeOfDayManager.Instance == null || material == null)
            return;

        if (TimeOfDayManager.Instance.IsNight())
        {
            if (nightTexture != null && material.mainTexture != nightTexture)
            {
                material.mainTexture = nightTexture;
            }

            if (emissionMap != null)
            {
                material.EnableKeyword("_EMISSION");
                material.SetTexture("_EmissionMap", emissionMap);
                material.SetColor("_EmissionColor", Color.white); // Or any glow color
            }
        }
        else
        {
            if (dayTexture != null && material.mainTexture != dayTexture)
            {
                material.mainTexture = dayTexture;
            }

            material.DisableKeyword("_EMISSION");
            material.SetTexture("_EmissionMap", null);
            material.SetColor("_EmissionColor", Color.black);
        }
    }
}
