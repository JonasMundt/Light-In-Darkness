using UnityEngine;

//Level 1 Button Logik mit merken

public class MemoryButtonTrigger : MonoBehaviour
{
    public MemoryPlatformGroup platformGroup;
    public CameraZoomTrigger cameraZoom;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (platformGroup != null)
            platformGroup.ShowPlatforms();

        if (cameraZoom != null)
            cameraZoom.ZoomOut();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (platformGroup != null)
            platformGroup.HidePlatforms();

        if (cameraZoom != null)
            cameraZoom.ZoomBack();
    }
}