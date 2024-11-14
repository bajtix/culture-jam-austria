using UnityEngine;

public class PlayerBrush : MonoBehaviour {
    [SerializeField] private float m_footstepDistance;
    [SerializeField] private Texture2D m_print;
    [SerializeField] private DrawingSurface m_surface;
    [SerializeField] private float m_strength;
    [SerializeField] private float m_scale;

    private Vector3 m_lastPosition;
    private bool m_left;


    private void FixedUpdate() {
        if ((m_lastPosition - transform.position).sqrMagnitude < m_footstepDistance) return;
        m_lastPosition = transform.position;

        if (!Physics.Raycast(transform.position, Vector3.down, out var hit, 2)) return;
        if (m_surface == null) {
            m_surface = hit.collider.GetComponent<DrawingSurface>();
            return;
        }
        m_surface.AddTextureMark(m_print, hit.textureCoord - new Vector2(transform.right.x * (m_left ? 0.005f : -0.005f), transform.right.z * (m_left ? 0.005f : -0.005f)), transform.rotation.eulerAngles.y * Mathf.Deg2Rad * (m_left ? 1 : -1), new Vector2(m_scale * (m_left ? 1 : -1), m_scale), m_strength);
        m_left = !m_left;
    }
}
