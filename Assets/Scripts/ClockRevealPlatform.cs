using UnityEngine;

//Level 5 Hauptlogik

public class ClockRevealPlatform : MonoBehaviour
{
    [Header("Clock Reference")]
    public ClockRhythmController clockController;

    private SpriteRenderer[] spriteRenderers;
    private bool lastVisibleState;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);

        if (clockController == null)
        {
            clockController = FindFirstObjectByType<ClockRhythmController>();
        }
    }

    private void Start()
    {
        if (clockController == null)
        {
            Debug.LogWarning($"[{nameof(ClockRevealPlatform)}] Kein ClockRhythmController gefunden auf {gameObject.name}");
            return;
        }

        lastVisibleState = clockController.ArePlatformsVisible();
        SetVisible(lastVisibleState);
    }

    private void Update()
    {
        if (clockController == null)
            return;

        bool currentVisibleState = clockController.ArePlatformsVisible();

        if (currentVisibleState == lastVisibleState)
            return;

        lastVisibleState = currentVisibleState;
        SetVisible(currentVisibleState);
    }

    //Sichtbarkeit if foreach loop
    private void SetVisible(bool isVisible)
    {
        if (spriteRenderers == null || spriteRenderers.Length == 0)
            return;

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            if (sr != null)
            {
                sr.enabled = isVisible;
            }
        }
    }
}