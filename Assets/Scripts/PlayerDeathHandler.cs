using System.Collections;
using UnityEngine;

    //Tot des MainCharacters Logik

public class PlayerDeathHandler : MonoBehaviour
{
    [Header("Respawn")]
    public Transform respawnPoint;
    public float respawnDelay = 0.6f;

    [Header("Animation")]
    public Animator bodyAnimator;
    public GameObject eyesObject;

    private bool isDead = false;

    private Rigidbody2D rb;
    private Collider2D[] playerColliders;
    private PlayerController2D playerController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerColliders = GetComponentsInChildren<Collider2D>(true);
        playerController = GetComponent<PlayerController2D>();
    }

    public void Die()
    {
        if (isDead) return;
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        isDead = true;

        //Death Sound
        if (SimpleAudioManager.Instance != null)
        {
            SimpleAudioManager.Instance.PlayDeath();
        }

        //Bewegung stoppen
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }

        //Steuerung deaktivieren
        if (playerController != null)
            playerController.enabled = false;

        //Collider deaktivieren
        foreach (Collider2D col in playerColliders)
        {
            col.enabled = false;
        }

        //Augen ausblenden
        if (eyesObject != null)
            eyesObject.SetActive(false);

        //Death Animation triggern
        if (bodyAnimator != null)
            bodyAnimator.SetTrigger("Die");

        yield return new WaitForSeconds(respawnDelay);

        //Respawn Position
        if (respawnPoint != null)
            transform.position = respawnPoint.position;

        //Respawn Sound
        if (SimpleAudioManager.Instance != null)
        {
            SimpleAudioManager.Instance.PlayRespawn();
        }

        //Animator resetten
        if (bodyAnimator != null)
        {
            bodyAnimator.Rebind();
            bodyAnimator.Update(0f);
        }

        //Augen wieder einblenden
        //Blink Logik ist nicht mehr drin!!!
        if (eyesObject != null)
            eyesObject.SetActive(true);

        //Collider wieder aktivieren
        foreach (Collider2D col in playerColliders)
        {
            col.enabled = true;
        }

        if (rb != null)
        {
            rb.simulated = true;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        if (playerController != null)
            playerController.enabled = true;

        isDead = false;
    }
}