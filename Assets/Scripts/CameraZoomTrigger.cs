using UnityEngine;

// Main Camera zoom out

public class CameraZoomTrigger : MonoBehaviour
{
    public Camera mainCamera;
    public CameraFollowLook cameraFollowLook;

    [Header("Zoom")]
    public float normalSize = 7f;
    public float zoomedOutSize = 10f;
    public float zoomSpeed = 3f;

    [Header("Room Focus")]
    public Transform focusPoint;
    public BoxCollider2D focusBounds;

    private float targetSize;

    void Start()
    {
        targetSize = normalSize;

        if (mainCamera != null)
            mainCamera.orthographicSize = normalSize;
    }

    void Update()
    {
        if (mainCamera == null) return;

        mainCamera.orthographicSize = Mathf.Lerp(
            mainCamera.orthographicSize,
            targetSize,
            Time.deltaTime * zoomSpeed
        );
    }

    //OUTZOOMEN
    public void ZoomOut()
    {
        targetSize = zoomedOutSize;

        if (cameraFollowLook != null && focusPoint != null)
        {
            cameraFollowLook.SetTemporaryFocus(focusPoint, focusBounds);
        }
    }
    //BACKZOOMEN
    public void ZoomBack()
    {
        targetSize = normalSize;

        if (cameraFollowLook != null)
        {
            cameraFollowLook.ClearTemporaryFocus();
        }
    }
}