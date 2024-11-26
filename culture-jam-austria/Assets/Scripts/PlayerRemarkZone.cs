using UnityEngine;
using NaughtyAttributes;

public class PlayerRemarkZone : MonoBehaviour {
    [BoxGroup("Activation")][SerializeField] private StageSettings m_whichStage;
    [BoxGroup("Activation")][SerializeField] private float m_minimumStay = 0.5f;
    [BoxGroup("Activation")][SerializeField] private bool m_oneShot;
    [SerializeField] private PlayerVoiceline m_voiceline;
    [SerializeField] private Transform m_suggestedLook;
    [SerializeField] private float m_speedModifier = 1;

    private bool m_played = false;
    private float m_stay = 0;
    private bool m_inRange = false;


    private void OnTriggerEnter(Collider col) {
        if (!col.CompareTag("Player")) return;
        m_inRange = true;
    }

    private void OnTriggerLeave(Collider col) {
        if (!col.CompareTag("Player")) return;
        m_inRange = false;
    }

    private void AddModifiers() {
        if (m_suggestedLook) {
            Game.Player.Controller.AddViewModifier(gameObject.name, m_suggestedLook.position, 0.9f);
        }
        if (m_speedModifier != 1) {
            Game.Player.Controller.AddSpeedModifier(gameObject.name, m_speedModifier);
        }
    }

    private void RemoveModifiers() {
        if (m_suggestedLook) {
            Game.Player.Controller.RemoveViewModifier(gameObject.name);
        }
        if (m_speedModifier != 1) {
            Game.Player.Controller.RemoveSpeedModifier(gameObject.name);
        }
    }

    private void FixedUpdate() {
        if (!m_inRange) return;
        m_stay += Time.fixedDeltaTime;

        if (m_stay >= m_minimumStay) {
            //play
            if (m_whichStage != null && m_whichStage != Game.Controller.CurrentStage) return;
            if (m_oneShot && m_played) return;

            //Game.Player.Cutscene.PlayVoiceline(m_voiceline);
            Debug.Log("rem " + m_voiceline.text);
            AddModifiers();
            Invoke(nameof(RemoveModifiers), m_voiceline.Duration);

            m_played = true;
            m_inRange = false;
        }

    }
}