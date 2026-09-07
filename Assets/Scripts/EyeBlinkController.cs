using UnityEngine;
using System.Collections;

//IST NICHT MEHR IMPLEMENTIERT!!!
//DER CHARAKTER BLINZELT NICHT MEHR!!!

public class EyeBlinkController : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite eyeOpenSprite;
    public Sprite eyeHalfSprite;
    public Sprite eyeClosedSprite;

    [Header("References")]
    public SpriteRenderer spriteRenderer;
    public EyePlatformGroup platformGroup;

    [Header("Timing")]
    public float openDuration = 2f;
    public float halfDuration = 0.4f;
    public float closedDuration = 1.2f;

    private void Start()
    {
        StartCoroutine(EyeLoop());
    }

    private IEnumerator EyeLoop()
    {
        while (true)
        {
            SetEyeState(eyeOpenSprite, true);
            yield return new WaitForSeconds(openDuration);

            SetEyeState(eyeHalfSprite, true);
            yield return new WaitForSeconds(halfDuration);

            SetEyeState(eyeClosedSprite, false);
            yield return new WaitForSeconds(closedDuration);

            SetEyeState(eyeHalfSprite, true);
            yield return new WaitForSeconds(halfDuration);
        }
    }

    private void SetEyeState(Sprite eyeSprite, bool platformsActive)
    {
        if (spriteRenderer != null && eyeSprite != null)
            spriteRenderer.sprite = eyeSprite;

        if (platformGroup != null)
            platformGroup.SetPlatformsActive(platformsActive);
    }
}