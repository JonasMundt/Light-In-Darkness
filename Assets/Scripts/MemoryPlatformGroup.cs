using UnityEngine;

//Level 1 Plattformen Logik mit Memory

public class MemoryPlatformGroup : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;

    void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Start()
    {
        HidePlatforms();
    }

    //Plattformen da
    public void ShowPlatforms()
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.enabled = true;
        }
    }

    //Plattformen weg
    public void HidePlatforms()
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.enabled = false;
        }
    }
}