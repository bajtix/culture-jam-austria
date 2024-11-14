using UnityEngine;

public class DrawingBrush : MonoBehaviour {
    public DrawingSurface surface;
    public float splatThreshold = 0.1f;
    public float maxHeight = 0.5f;
    public float radius = 0.05f;
    public float strength = 0.9f;
    public float shape = 0.9f;

    private Vector3 m_lastPosition;
    private Vector2 m_lastHit;

    private void FixedUpdate() {


        if ((m_lastPosition - transform.position).sqrMagnitude < splatThreshold) return;
        m_lastPosition = transform.position;

        if (!Physics.Raycast(transform.position, Vector3.down, out var hit, maxHeight)) return;
        if (surface == null) {
            surface = hit.collider.GetComponent<DrawingSurface>();
            m_lastHit = hit.textureCoord;
            return;
        }
        surface.AddLineMark(hit.textureCoord, m_lastHit, radius, shape, strength);
        m_lastHit = hit.textureCoord;
    }
}