using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Level6CutsceneController : MonoBehaviour
{
    [Header("References")]
    
    // Steuerung des Spielers
    public PlayerController2D playerController;

    // Position des Spielers
    public Transform playerTransform;
    public GameObject mainCharacterEyes;
    public Transform walkTarget;

    [Header("Animators")]
    public Animator endCharacterAnimator;
    public Animator mainCharacterAnimator;

    [Header("Dialogue")]
    public AutoDialogue autoDialogue;

    [Header("Fade UI")]
    //UI Elemente für den weißen Fade + Texte + Button
    public Image whiteFadeImage;
    public TMP_Text continuationText;
    public TMP_Text thanksText;
    public GameObject mainMenuButton;

    [Header("Timing")]
    // Alle Timings für die Cutscene
    //Easy anpassbar in Unity/Inspector
    public float playerWalkSpeed = 2f;
    public float waitAfterDialogue = 0.8f;
    public float bowDuration = 4f;
    public float waitBeforeLookUp = 0.8f;
    public float waitAfterLookUp = 1.2f;
    public float fadeDuration = 3f;
    public float thanksDelay = 1.8f;

    private bool hasStarted = false; //Verhindert mehrfaches Starten der Cutscene

    private void Start()
    {
        //UI Elemente am Anfang ausblenden
        if (continuationText != null)
            continuationText.gameObject.SetActive(false);

        if (thanksText != null)
            thanksText.gameObject.SetActive(false);

        if (mainMenuButton != null)
            mainMenuButton.SetActive(false);

        //White Fade vorbereiten (Starter unsichtbar)
        if (whiteFadeImage != null)
        {
            RectTransform rect = whiteFadeImage.rectTransform;

            //Position des WhiteFadeImages
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);

            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = Vector2.zero; //Start unsichtbar

            //Farbe ist weiß
            whiteFadeImage.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Cutscene darf nur einmal starten
        if (hasStarted) return;

        //Nur loslegen wenn Spieler den Trigger betritt
        if (!other.CompareTag("Player")) return;

        hasStarted = true;
        StartCoroutine(CutsceneRoutine());
    }

    private IEnumerator CutsceneRoutine()
    {
        //Spieler Kontrolle deaktivieren
        if (playerController != null)
        {
            playerController.LockControl();
            playerController.StopMovement();
        }

        //Dialog starten und warten bis er fertig ist
        if (autoDialogue != null)
        {
            autoDialogue.StartDialogue();

            while (!autoDialogue.IsFinished)
                yield return null;
        }

        yield return new WaitForSeconds(waitAfterDialogue);

        //Endcharakter verbeugt sich
        if (endCharacterAnimator != null)
            endCharacterAnimator.SetTrigger("Bow");

        yield return new WaitForSeconds(bowDuration);

        //Spieler läuft automatisch zur Zielposition
        yield return StartCoroutine(MovePlayerToTarget());

        yield return new WaitForSeconds(waitBeforeLookUp);

        //Augen ausblenden
        //WAR EINE BLINZELN LOGIK!!!
        if (mainCharacterEyes != null)
            mainCharacterEyes.SetActive(false);

        //Beide schauen nach oben
        if (endCharacterAnimator != null)
            endCharacterAnimator.SetTrigger("LookUp");

        if (mainCharacterAnimator != null)
            mainCharacterAnimator.SetTrigger("LookUp");

        yield return new WaitForSeconds(waitAfterLookUp);

        //Weißer Fade startet
        yield return StartCoroutine(FadeToWhite());

        //Fortsetzung folgt anzeigen
        if (continuationText != null)
        {
            continuationText.gameObject.SetActive(true);
            continuationText.text = "Fortsetzung folgt";
        }

        yield return new WaitForSeconds(thanksDelay);

        //Text wechseln
        if (continuationText != null)
            continuationText.gameObject.SetActive(false);

        if (thanksText != null)
        {
            thanksText.gameObject.SetActive(true);
            thanksText.text = "Danke fürs Spielen";
        }

        //Button zum Hauptmenü anzeigen
        if (mainMenuButton != null)
            mainMenuButton.SetActive(true);
    }

    private IEnumerator MovePlayerToTarget()
    {
        //Sicherheitscheck
        if (playerTransform == null || walkTarget == null || playerController == null)
            yield break;

        Rigidbody2D rb = playerController.GetRigidbody();
        if (rb == null)
            yield break;

        Vector2 targetPos = new Vector2(walkTarget.position.x, walkTarget.position.y);

        //Solange Spieler noch nicht am Ziel ist → bewegen
        while (Vector2.Distance(rb.position, targetPos) > 0.08f)
        {
            Vector2 currentPos = rb.position;
            Vector2 direction = (targetPos - currentPos).normalized;

            //Spieler in richtige Richtung drehen
            if (direction.x > 0.01f)
                playerController.FaceRight();
            else if (direction.x < -0.01f)
                playerController.FaceLeft();

            //Bewegung berechnen
            Vector2 nextPos = Vector2.MoveTowards(
                currentPos,
                targetPos,
                playerWalkSpeed * Time.fixedDeltaTime
            );

            rb.MovePosition(nextPos);

            yield return new WaitForFixedUpdate();
        }

        //Finale exakte Position
        rb.MovePosition(targetPos);
        playerController.StopMovement();
    }

    private IEnumerator FadeToWhite()
    {
        if (whiteFadeImage == null)
            yield break;

        //Sound abspielen
        if (SimpleAudioManager.Instance != null)
        {
            SimpleAudioManager.Instance.PlayLightLevel6();
        }

        RectTransform rect = whiteFadeImage.rectTransform;

        float t = 0f;

        //Bildschirm komplett füllen nach dem Start
        Vector2 startSize = Vector2.zero;
        Vector2 endSize = new Vector2(Screen.width * 1.5f, Screen.height * 1.5f);

        rect.sizeDelta = startSize;

        //Fade Effekt
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float progress = Mathf.Clamp01(t / fadeDuration);

            rect.sizeDelta = Vector2.Lerp(startSize, endSize, progress);

            yield return null;
        }

        rect.sizeDelta = endSize;
    }

    public void BackToMainMenu()
    {
        //Lädt die Main Menu Szene
        SceneManager.LoadScene("Main_Menu");
    }
}