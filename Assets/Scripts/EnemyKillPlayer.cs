using UnityEngine;

//Einfacher Death on Touch vom Enemy
//Level 4 Death

public class EnemyKillPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerDeathHandler deathHandler = other.GetComponent<PlayerDeathHandler>();

        if (deathHandler != null)
        {
            deathHandler.Die();
        }
    }
}