using UnityEngine;

//Level 4 swing Lamp hin und her

public class SwingLamp : MonoBehaviour
{
    [Header("Swing Settings")]
    public float maxAngle = 25f;
    public float swingSpeed = 1.2f;

    private void Update()
    {
        float angle = Mathf.Sin(Time.time * swingSpeed) * maxAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}