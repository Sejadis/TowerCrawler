using UnityEngine;

public class CameraFrustum : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private void OnDrawGizmos()
    {
        if (cam == null) return;

        Gizmos.DrawFrustum(transform.position, cam.fieldOfView, cam.farClipPlane, cam.nearClipPlane, cam.aspect);
    }
}