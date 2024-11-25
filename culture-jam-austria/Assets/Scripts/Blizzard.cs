using NaughtyAttributes;
using UnityEngine;

public class Blizzard : MonoBehaviour {
    [SerializeField] private float m_target = 0.1f;
    [SerializeField] private float m_changeSpeed = 0.1f;
    [SerializeField] private float m_cameraBackgroundBrightness = 0.98f;

    [SerializeField] private Gradient m_fogColor;
    [SerializeField][NaughtyAttributes.MinMaxSlider(0, 100)] private Vector2 m_minimumFog;
    [SerializeField][NaughtyAttributes.MinMaxSlider(0, 100)] private Vector2 m_maximumFog;
    [SerializeField] private AnimationCurve m_snowStrength = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private AnimationCurve m_snowSpeed = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private ParticleSystem m_snow;
    [SerializeField] private float m_snowHeight = 5;
    [SerializeField] private float m_snowForward = 5;
    [SerializeField] private AnimationCurve m_noiseIntensity = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private Material m_fogScreenspace;

    [SerializeField][ReadOnly] private float m_intensity;

    public void SetIntensity(float s) {
        m_target = s;
    }

    private void Update() {
        m_snow.transform.position = Game.Player.transform.position + m_snowHeight * Vector3.up + Game.Player.transform.forward * m_snowForward;
        m_intensity = Mathf.Lerp(m_intensity, m_target, m_changeSpeed * Time.deltaTime);
        if (Mathf.Abs(m_intensity - m_target) > 0.05f)
            UpdateEffect();
    }

    private void UpdateEffect() {
        RenderSettings.fogStartDistance = Mathf.Lerp(m_minimumFog.x, m_maximumFog.x, m_intensity);
        RenderSettings.fogEndDistance = Mathf.Lerp(m_minimumFog.y, m_maximumFog.y, m_intensity);
        RenderSettings.fogColor = m_fogColor.Evaluate(m_intensity);
        Camera.main.backgroundColor = m_fogColor.Evaluate(m_intensity) * m_cameraBackgroundBrightness;

        var em = m_snow.emission;
        em.rateOverTime = m_snowStrength.Evaluate(m_intensity);

        var ma = m_snow.main;
        ma.startSpeedMultiplier = m_snowSpeed.Evaluate(m_intensity);
        m_fogScreenspace.SetFloat("_Noise_Intensity", m_noiseIntensity.Evaluate(m_intensity));
    }
}
