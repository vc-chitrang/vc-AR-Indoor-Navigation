using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineAnimator : MonoBehaviour {

    private LineRenderer lineRenderer;
    private Material lineMaterial;
    [SerializeField]private float speed = 1;
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineMaterial = lineRenderer.material;
    }

    void Update() {
        MoveOffset();
    }

    private void MoveOffset() {
        if (lineMaterial.mainTextureOffset.x >= 100) {
            lineMaterial.mainTextureOffset = Vector2.zero;
        }
        lineMaterial.mainTextureOffset += Vector2.left * Time.deltaTime * speed;
    }
}
