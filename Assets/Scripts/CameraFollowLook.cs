using UnityEngine;

//Normaler Camera Follow Look vom MainCharacter

[RequireComponent(typeof(Camera))]
public class CameraFollowLook : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.15f;

    [Header("Look")]
    public float lookOffsetY = 2.5f;
    public float holdTimeToLook = 2f;
    public float lookSmoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;

    private float holdUpTimer = 0f;
    private float holdDownTimer = 0f;

    private float currentLookY = 0f;
    private float lookVelocity = 0f;

    [Header("Temporary Focus")]
    public bool isInFocusMode = false;
    private Transform focusTarget;
    private BoxCollider2D focusBounds;

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (target == null && !isInFocusMode) return;

        Vector3 desiredPosition;

        if (isInFocusMode && focusTarget != null)
        {
            // Focus mode -> folgt nicht dem Spieler
            desiredPosition = new Vector3(
                focusTarget.position.x,
                focusTarget.position.y,
                transform.position.z
            );

            desiredPosition = ClampToBounds(desiredPosition);
        }
        else
        {
            // W Halten für nach oben gucken
            if (Input.GetKey(KeyCode.W))
                holdUpTimer += Time.deltaTime;
            else
                holdUpTimer = 0f;
            // S Halten für nach unten gucken
            if (Input.GetKey(KeyCode.S))
                holdDownTimer += Time.deltaTime;
            else
                holdDownTimer = 0f;

            float desiredLookY = 0f;

            if (holdUpTimer >= holdTimeToLook) desiredLookY = lookOffsetY;
            if (holdDownTimer >= holdTimeToLook) desiredLookY = -lookOffsetY;

            currentLookY = Mathf.SmoothDamp(currentLookY, desiredLookY, ref lookVelocity, lookSmoothTime);

            desiredPosition = new Vector3(
                target.position.x,
                target.position.y + currentLookY,
                transform.position.z
            );
        }

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }

    public void SetTemporaryFocus(Transform newFocusTarget, BoxCollider2D newFocusBounds = null)
    {
        isInFocusMode = true;
        focusTarget = newFocusTarget;
        focusBounds = newFocusBounds;

        // Reset look timers. Ist dann nicht buggy
        holdUpTimer = 0f;
        holdDownTimer = 0f;
        currentLookY = 0f;
    }

    public void ClearTemporaryFocus()
    {
        isInFocusMode = false;
        focusTarget = null;
        focusBounds = null;
    }

    private Vector3 ClampToBounds(Vector3 desiredPosition)
    {
        if (focusBounds == null || cam == null || !cam.orthographic)
            return desiredPosition;

        Bounds bounds = focusBounds.bounds;

        float cameraHalfHeight = cam.orthographicSize;
        float cameraHalfWidth = cam.aspect * cameraHalfHeight;

        float minX = bounds.min.x + cameraHalfWidth;
        float maxX = bounds.max.x - cameraHalfWidth;
        float minY = bounds.min.y + cameraHalfHeight;
        float maxY = bounds.max.y - cameraHalfHeight;

        // Camera logik mit center
        float clampedX = (minX > maxX) ? bounds.center.x : Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = (minY > maxY) ? bounds.center.y : Mathf.Clamp(desiredPosition.y, minY, maxY);

        return new Vector3(clampedX, clampedY, desiredPosition.z);
    }
}