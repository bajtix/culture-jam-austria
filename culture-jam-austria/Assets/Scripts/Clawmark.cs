using NaughtyAttributes;
using UnityEngine;

public class Clawmark : MonoBehaviour {
    private DrawingSurface m_surface;
    [SerializeField] private float m_maxDistance = 4f;
    [SerializeField][MinMaxSlider(0.1f, 20f)] private Vector2 m_scaleRandom;
    [SerializeField][MinMaxSlider(0f, 1f)] private Vector2 m_strengthRandom;
    [SerializeField] private Texture m_print;
    [SerializeField] private bool m_placeOnAwake = true;
    [SerializeField] private SoundBite m_scratchSound;

    private void PlaceMark(Vector3 pos, Vector2 sc, float rot, float str) {
        if (!Physics.Raycast(pos, Vector3.down, out var hit, m_maxDistance)) return;
        m_surface = hit.collider.GetComponent<DrawingSurface>();
        if (m_surface == null) return;
        m_surface.AddTextureMark(m_print, hit.textureCoord, rot, sc, str);
    }

    private void Start() {
        if (m_placeOnAwake)
            Place();
    }

    public void Place() {
        Place(transform.position);
    }

    public void Place(Vector3 where) {
        Game.SexMan.Play(m_scratchSound, where, 0.9f);
        PlaceMark(where, Vector2.one * Random.Range(m_scaleRandom.x, m_scaleRandom.y), Random.Range(0, 360f), Random.Range(m_strengthRandom.x, m_strengthRandom.y));
    }
}
