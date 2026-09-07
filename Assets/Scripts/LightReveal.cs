using UnityEngine;

//Level 4 Plattform Lampe Logik 2

public class LightReveal : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        SetVisible(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LightCone"))
        {
            SetVisible(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("LightCone"))
        {
            SetVisible(false);
        }
    }

    //Unsichtbar machen
    private void SetVisible(bool isVisible)
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.enabled = isVisible;
        }
    }
}