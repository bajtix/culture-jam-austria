using UnityEngine;

public class Blizzard : MonoBehaviour {
    [SerializeField] private float m_intensity = 0.1f;

    [SerializeField] private Gradient m_fogColor;
    [SerializeField][NaughtyAttributes.MinMaxSlider(0, 100)] private Vector2 m_minimumFog;
    [SerializeField][NaughtyAttributes.MinMaxSlider(0, 100)] private Vector2 m_maximumFog;
    [SerializeField] private AnimationCurve m_snowStrength = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private ParticleSystem m_snow;

    private void Update() {
        RenderSettings.fogStartDistance = Mathf.Lerp(m_minimumFog.x, m_maximumFog.x, m_intensity);
        RenderSettings.fogEndDistance = Mathf.Lerp(m_minimumFog.y, m_maximumFog.y, m_intensity);
        RenderSettings.fogColor = m_fogColor.Evaluate(m_intensity);
        Camera.main.backgroundColor = m_fogColor.Evaluate(m_intensity);

        var em = m_snow.emission;
        em.rateOverTime = m_snowStrength.Evaluate(m_intensity);
    }
}
