using UnityEngine;


// NICHT MEHR IMPLEMENTIERT!!!!!
// BlINZELN WURDE RAUSGENOMMEN!


public class BlinkController : MonoBehaviour
{
    [Header("Blink")]
    public float blinkStartDelay = 20f;
    public float blinkRepeatRate = 20f;

    [Header("Idle Eye Offset")]
    public bool useIdleMotion = true;
    public Vector3 frame1LocalPos;
    public Vector3 frame2LocalPos;
    public Vector3 frame3LocalPos;
    public Vector3 frame4LocalPos;
    public float frameDuration = 0.15f;

    private Animator animator;
    private Vector3[] idlePositions;
    private int currentFrame = 0;
    private float timer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating(nameof(Blink), blinkStartDelay, blinkRepeatRate);

        idlePositions = new Vector3[4]
        {
            frame1LocalPos,
            frame2LocalPos,
            frame3LocalPos,
            frame4LocalPos
        };

        if (useIdleMotion)
        {
            transform.localPosition = idlePositions[0];
        }
    }

    void Update()
    {
        if (!useIdleMotion || frameDuration <= 0f) return;

        timer += Time.deltaTime;

        if (timer >= frameDuration)
        {
            timer = 0f;
            currentFrame++;
            if (currentFrame >= idlePositions.Length)
                currentFrame = 0;

            transform.localPosition = idlePositions[currentFrame];
        }
    }

    

    void Blink()
    {
        if (animator != null)
        {
            animator.SetTrigger("Blink");
        }
    }
}