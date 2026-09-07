using UnityEngine;
using System.Collections;

//Level 3 Wolken Controller

public class ThunderCloudController : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite cloudNormalSprite;
    public Sprite cloudHalfLightningSprite;
    public Sprite cloudFullLightningSprite;

    [Header("References")]
    public SpriteRenderer spriteRenderer;
    public ThunderPlatformGroup platformGroup;

    [Header("Timing")]
    public float darkDurationMin = 2f;
    public float darkDurationMax = 4f;
    public float halfFlashDuration = 0.08f;
    public float fullFlashDuration = 0.12f;
    public float timeBetweenHalfAndFull = 0.05f;
    private int thunderCounter = 0;

    private void Start()
    {
        StartCoroutine(ThunderLoop());
    }

    private IEnumerator ThunderLoop()
    {
        //Start ist dunkel
        SetState(cloudNormalSprite, false);

        while (true)
        {
            float waitTime = Random.Range(darkDurationMin, darkDurationMax);
            yield return new WaitForSeconds(waitTime);

            //halb flash
            SetState(cloudHalfLightningSprite, true);
            yield return new WaitForSeconds(halfFlashDuration);

            //kurze dunkel Pause
            SetState(cloudNormalSprite, false);
            yield return new WaitForSeconds(timeBetweenHalfAndFull);

            //volles Flash
            SetState(cloudFullLightningSprite, true);

            thunderCounter++;

            //Nur jeder dritte Blitz spielt Sound
            if (thunderCounter % 3 == 0)
            {
                if (SimpleAudioManager.Instance != null)
                {
                    SimpleAudioManager.Instance.PlayThunder();
                }
            }

            yield return new WaitForSeconds(fullFlashDuration);

            //zurück zum dunkel
            SetState(cloudNormalSprite, false);
        }
    }

    private void SetState(Sprite cloudSprite, bool platformsVisible)
    {
        if (spriteRenderer != null && cloudSprite != null)
            spriteRenderer.sprite = cloudSprite;

        if (platformGroup != null)
            platformGroup.SetPlatformsVisible(platformsVisible);
    }
}