using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerStatusInformer : PlayerComponent {
    [SerializeField] private Material m_postprocessing;
    [SerializeField] private AnimationCurve m_disturbanceIntensity;
    [SerializeField] private AnimationCurve m_noiseIntensity;
    [SerializeField] private AnimationCurve m_redFilterIntensity;
    [SerializeField] private float m_dangerNotifySpeed = 1f;
	[SerializeField] private Volume m_redVolume;
    private float m_danger;
    private void Update() {
        //  stamina bar
        Game.UI.PlayerStatus.StaminaSetVisible(Player.Controller.Stamina < 1);
        Game.UI.PlayerStatus.StaminaSet(Player.Controller.Stamina);
        Game.UI.PlayerStatus.StaminaSetTired(Player.Controller.IsTired);

        // schizo noise
        m_danger = Mathf.Lerp(m_danger, Game.Controller.IsPlayerSafe ? 0.2f : 1f, Time.deltaTime * m_dangerNotifySpeed);
        m_postprocessing.SetFloat("_Wiggle_Noise_Intensity", 0.01f * m_disturbanceIntensity.Evaluate(Game.Controller.StormHuntingPercent) * m_danger);
        m_postprocessing.SetFloat("_White_Noise_Intensity", m_noiseIntensity.Evaluate(Game.Controller.StormHuntingPercent) * m_danger);

		m_redVolume.weight = m_redFilterIntensity.Evaluate(Game.Controller.StormHuntingPercent) * m_danger;
    }
}
