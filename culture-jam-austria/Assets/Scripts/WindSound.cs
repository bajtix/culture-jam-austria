using NaughtyAttributes;
using UnityEngine;

public class WindSound : MonoBehaviour {
    [SerializeField] private AudioSource[] m_locationSources;
    [SerializeField] private float m_changeLocationSpeed = 2;
    [SerializeField][ReadOnly] private int m_currentLocation;

    private void Update() {
        for (int i = 0; i < m_locationSources.Length; i++) {
            m_locationSources[i].volume = Mathf.Lerp(m_locationSources[i].volume, i == m_currentLocation ? 1 : 0f, Time.deltaTime * m_changeLocationSpeed);
        }
    }

    public void SetLocation(int i) { m_currentLocation = i; }
}
