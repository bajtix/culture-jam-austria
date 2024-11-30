using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Audio;

public class WindSound : MonoBehaviour {
    [SerializeField] private AudioSource[] m_locationSources;
    [SerializeField] private AudioSource m_stormSource;
    [SerializeField] private AudioSource m_monsterSource;
    [SerializeField] private float m_changeLocationSpeed = 2;
    [SerializeField][ReadOnly] private int m_currentLocation;
    [SerializeField] private AudioMixer m_winds;

    private void Update() {
        for (int i = 0; i < m_locationSources.Length; i++) {
            m_locationSources[i].volume = Mathf.Lerp(m_locationSources[i].volume, i == m_currentLocation ? 1 : 0f, Time.deltaTime * m_changeLocationSpeed);
        }

        m_winds.SetFloat("muteWind", Game.Controller.IsPlayerSafe ? -2 : -80);

        m_stormSource.volume = Game.Controller.StormHuntingPercent;
        m_monsterSource.volume = Game.Controller.StormDeadlyPercent * (Game.Controller.IsMonsterHunting ? 1 : 0);
    }

    public void SetLocation(int i) { m_currentLocation = i; }
}
