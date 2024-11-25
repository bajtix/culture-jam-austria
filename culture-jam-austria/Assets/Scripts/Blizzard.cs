using NaughtyAttributes;
using UnityEngine;

[ExecuteAlways]
public class Blizzard : MonoBehaviour {
    [SerializeField] private float m_target = 0.1f;
    [SerializeField] private float m_changeSpeed = 0.1f;

    [BoxGroup("Fog")][SerializeField] private AnimationCurve m_cameraBackgroundBrightness = AnimationCurve.Constant(0, 1, 1.05f);
    [BoxGroup("Fog")][SerializeField] private AnimationCurve m_fogTransition = AnimationCurve.Linear(0, 1, 0, 1);
    [BoxGroup("Fog")][SerializeField] private Gradient m_fogColor;
    [BoxGroup("Fog")][SerializeField][MinMaxSlider(0, 100)] private Vector2 m_minimumFog;
    [BoxGroup("Fog")][SerializeField][MinMaxSlider(0, 100)] private Vector2 m_maximumFog;
    [BoxGroup("Snow")][SerializeField] private ParticleSystem m_snow;
    [BoxGroup("Snow")][SerializeField] private AnimationCurve m_snowStrength = AnimationCurve.Linear(0, 0, 1, 1);
    [BoxGroup("Snow")][SerializeField] private AnimationCurve m_snowSpeed = AnimationCurve.Linear(0, 0, 1, 1);
    [BoxGroup("Snow")][SerializeField] private float m_snowHeight = 5;
    [BoxGroup("Snow")][SerializeField] private float m_snowForward = 5;
    [BoxGroup("Wind")][SerializeField] private TreeShaderWind m_wind;
    [BoxGroup("Wind")][SerializeField] private AnimationCurve m_windIntensity = AnimationCurve.Linear(0, 1, 1, 1.6f);
    [SerializeField] private AnimationCurve m_noiseIntensity = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private Material m_fogScreenspace;

    [SerializeField][ReadOnly] private float m_intensity;

    public void SetIntensity(float s) {
        m_target = s;
    }

    private void Start() {
        UpdateEffect();
    }

    private void Update() {
        var tracked = /* Game.Player.transform */ Camera.main.transform;
        m_snow.transform.position = tracked.position + m_snowHeight * Vector3.up + tracked.forward * m_snowForward;
        m_intensity = Mathf.Lerp(m_intensity, m_target, m_changeSpeed * Time.deltaTime);
        //if (Mathf.Abs(m_intensity - m_target) > 0.05f)
        UpdateEffect();
    }

    private void UpdateEffect() {
        RenderSettings.fogStartDistance = Mathf.Lerp(m_minimumFog.x, m_maximumFog.x, m_fogTransition.Evaluate(m_intensity));
        RenderSettings.fogEndDistance = Mathf.Lerp(m_minimumFog.y, m_maximumFog.y, m_fogTransition.Evaluate(m_intensity));
        RenderSettings.fogColor = m_fogColor.Evaluate(m_intensity);
        Camera.main.backgroundColor = m_fogColor.Evaluate(m_intensity) * m_cameraBackgroundBrightness.Evaluate(m_intensity);

        var em = m_snow.emission;
        em.rateOverTime = m_snowStrength.Evaluate(m_intensity);

        var ma = m_snow.main;
        ma.startSpeedMultiplier = m_snowSpeed.Evaluate(m_intensity);
        m_fogScreenspace.SetFloat("_Noise_Intensity", m_noiseIntensity.Evaluate(m_intensity));

        m_wind.SetMultipliers(1, m_windIntensity.Evaluate(m_intensity));
    }
}
