using UnityEngine;

    //Plattformen sichtbar/unsichtbar Logik

public class ThunderPlatformGroup : MonoBehaviour
{
    public GameObject[] platforms;

    public void SetPlatformsVisible(bool isVisible)
    {
        foreach (GameObject platform in platforms)
        {
            if (platform == null) continue;

            SpriteRenderer[] spriteRenderers = platform.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.enabled = isVisible;
            }
        }
    }
}