using UnityEngine;

//Level 4 Enemy geht von Position zu Position
//Positionen sind in Unity geregelt

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;

    private Transform currentTarget;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        currentTarget = pointB;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (pointA == null || pointB == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            currentTarget.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, currentTarget.position) < 0.05f)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA;
        }

        // Flip sprite depending on direction
        if (spriteRenderer != null)
        {
            if (currentTarget.position.x > transform.position.x)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
        }
    }
}