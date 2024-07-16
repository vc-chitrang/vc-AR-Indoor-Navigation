using UnityEngine;
using UnityEngine.AI;

public class NavMeshPathDrawer : MonoBehaviour {
    [SerializeField]
    private Transform sourceTransform;
    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Camera topDownCamera;
    [SerializeField]
    private Vector3 cameraOffset;

    private NavMeshPath navMeshPath;

    void Start() {
        navMeshPath = new NavMeshPath();
        lineRenderer.positionCount = 0;
    }

    void Update() {
        DrawPath();
        FollowSourceTransform();
    }

    private void DrawPath() {
        if (sourceTransform == null || targetTransform == null || lineRenderer == null) {
            return;
        }

        NavMesh.CalculatePath(sourceTransform.position, targetTransform.position, NavMesh.AllAreas, navMeshPath);

        if (navMeshPath.status == NavMeshPathStatus.PathComplete) {
            lineRenderer.positionCount = navMeshPath.corners.Length;
            lineRenderer.SetPositions(navMeshPath.corners);
            lineRenderer.enabled = true;
        } else {
            lineRenderer.enabled = false;
        }
    }

    private void FollowSourceTransform() {
        if (topDownCamera == null || sourceTransform == null) {
            return;
        }

        // Follow the source transform's position with an offset
        topDownCamera.transform.position = sourceTransform.position + cameraOffset;

        // Rotate the camera to match the source transform's rotation if needed
        topDownCamera.transform.rotation = Quaternion.Euler(90f, sourceTransform.eulerAngles.y, 0f);
    }
}
