using UnityEngine;

//Level 2 Auge ist auf -> Plattformen sind da
//Level 2 Auge ist zu -> Plattformen sind weg

public class EyePlatformGroup : MonoBehaviour
{
    public GameObject[] platforms;

    public void SetPlatformsActive(bool isActive)
    {
        foreach (GameObject platform in platforms)
        {
            if (platform == null) continue;

            // Disable/enable alle SpirteRenderer auf diesem Objekt
            SpriteRenderer[] spriteRenderers = platform.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.enabled = isActive;
            }

            // Disable/enable alle Collider2D auf diesem Objekt und Child
            Collider2D[] colliders = platform.GetComponentsInChildren<Collider2D>(true);
            foreach (Collider2D col in colliders)
            {
                col.enabled = isActive;
            }
        }
    }
}