using NaughtyAttributes;
using UnityEngine;

public class PlayerBrush : PlayerComponent {
    [SerializeField] private Texture2D m_print;
    [SerializeField] private float m_footstepDistance;
    [SerializeField] private float m_strength;
    [SerializeField] private float m_scale;
    [SerializeField] private float m_footstepOffset = 0.05f;
    [SerializeField][ReadOnly] private DrawingSurface m_surface;

    private Vector3 m_lastPosition;
    private bool m_left;

    private void FixedUpdate() {
        if ((m_lastPosition - transform.position).sqrMagnitude < m_footstepDistance) return;
        m_lastPosition = transform.position;

        void PlaceFootstep(Vector3 pos, Vector2 sc, float rot) {
            if (!Physics.Raycast(pos, Vector3.down, out var hit, 2)) return;
            m_surface = hit.collider.GetComponent<DrawingSurface>();
            m_surface.AddTextureMark(m_print, hit.textureCoord, rot, sc, m_strength);
        }

        var stepPosition = transform.position - transform.right * (m_left ? 1 : -1) * m_footstepOffset;
        PlaceFootstep(stepPosition, new Vector2(m_left ? m_scale : -m_scale, m_scale), m_left ? transform.rotation.eulerAngles.y : -transform.rotation.eulerAngles.y);



        m_left = !m_left;
    }
}
