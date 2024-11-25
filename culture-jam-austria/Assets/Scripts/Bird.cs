using NaughtyAttributes;
using UnityEngine;


public class Bird : MonoBehaviour {
    [SerializeField][MinMaxSlider(0, 40f)] private Vector2 m_timeOffset;
    [SerializeField][MinMaxSlider(0.5f, 1.5f)] private Vector2 m_pitch;

    private float m_next;

    [SerializeField] private AudioSource m_source;

    private void Play() {
        m_source.pitch = Random.Range(m_pitch.x, m_pitch.y);
        m_source.loop = false;
        m_source.Play();
    }

    private void Start() {
        m_next = Random.Range(m_timeOffset.x, m_timeOffset.y);
    }

    private void FixedUpdate() {
        if (m_next > 0) {
            m_next -= Time.fixedDeltaTime;
        } else {
            Play();
            m_next = Random.Range(m_timeOffset.x, m_timeOffset.y);

        }
    }

}
