using UnityEngine;
using NaughtyAttributes;

public class RemarkZone : MonoBehaviour {
    [BoxGroup("Activation")][SerializeField] private StageSettings m_whichStage;
    [BoxGroup("Activation")][SerializeField] private float m_minimumStay = 0.5f;
    [BoxGroup("Activation")][SerializeField] private bool m_oneShot;
    [SerializeField] private PlayerVoiceline m_voiceline;
    [BoxGroup("Look")][SerializeField] private Transform m_suggestedLook;
    [BoxGroup("Look")][SerializeField] private float m_lookStrength = 0.6f;
    [BoxGroup("Look")][SerializeField] private float m_lookSpeed = 2f;
    [SerializeField] private float m_speedModifier = 1;

    private bool m_played = false;
    private float m_stay = 0;
    private bool m_inRange = false;
    private bool m_isPlaying = false;
    [SerializeField][ReadOnly] private float m_actualVm = 0f;


    private void OnTriggerEnter(Collider col) {
        if (!col.CompareTag("Player")) return;
        m_inRange = true;
    }

    public void RemotelyActivate() {
        m_inRange = true;
        m_stay = m_minimumStay;
    }

    private void OnTriggerLeave(Collider col) {
        if (!col.CompareTag("Player")) return;
        m_inRange = false;
    }

    private void StartPlaying() {
        m_isPlaying = true;
        if (m_speedModifier != 1) {
            Game.Player.Controller.AddSpeedModifier(gameObject.name, m_speedModifier);
        }
    }

    private void StopPlaying() {
        m_isPlaying = false;

        if (m_speedModifier != 1) {
            Game.Player.Controller.RemoveSpeedModifier(gameObject.name);
        }
    }

    private void FixedUpdate() {
        m_actualVm = Mathf.Lerp(m_actualVm, m_isPlaying ? m_lookStrength : 0, Time.fixedDeltaTime * m_lookSpeed);

        if (m_suggestedLook) {
            if (m_actualVm > 0.01) {
                Game.Player.Controller.AddViewModifier(gameObject.name, m_suggestedLook.position, m_actualVm);
            } else if (Game.Player.Controller.IsViewModifier(gameObject.name)) {
                Game.Player.Controller.RemoveViewModifier(gameObject.name);
            }
        }

        if (!m_inRange) return;
        m_stay += Time.fixedDeltaTime;

        if (m_stay >= m_minimumStay) {
            //play
            if (m_whichStage != null && m_whichStage != Game.Controller.CurrentStage) return;
            if (m_oneShot && m_played) return;

            Game.Player.Cutscene.PlayVoiceline(m_voiceline);
            Debug.Log("rem " + m_voiceline.text);
            StartPlaying();
            Invoke(nameof(StopPlaying), m_voiceline.Duration);

            m_played = true;
            m_inRange = false;
            m_stay = 0;
        }

    }
}