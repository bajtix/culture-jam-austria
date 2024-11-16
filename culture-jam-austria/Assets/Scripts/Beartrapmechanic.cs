using UnityEngine;

public class Beartrapmechanic : MonoBehaviour, IInteractable {
    [SerializeField] private PlayerController m_playerController;
    private float m_trapworktime = 5f;
    private bool m_isTrapActivated = false;
    private float m_timeSinceActivated = 0f;

    string IInteractable.Tooltip => "Defuse trap";

    private void Start() {

    }

    private void Update() {
        if (m_isTrapActivated) {
            m_timeSinceActivated += Time.deltaTime;
            if (m_timeSinceActivated >= m_trapworktime) {
                m_playerController.RemoveSpeedModifier("Stop");
                m_isTrapActivated = false;
                m_timeSinceActivated = 0f;
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) return;
        Trap_effect();
    }

    private void Trap_effect() {
        m_isTrapActivated = true;
        m_timeSinceActivated = 0f;
        m_playerController.AddSpeedModifier("Stop", 0f);
    }

    void IInteractable.HighlightBegin(Player player) {

    }
    void IInteractable.HighlightEnd(Player player) {

    }
    bool IInteractable.CanInteract(Player player) {
        return !m_isTrapActivated;
    }
    bool IInteractable.CanStopInteraction(Player player) {
        return true;
    }
    bool IInteractable.InteractionOver(Player player) {
        return false;
    }
    void IInteractable.InteractionStart(Player player) {
        print("Interakcja start");
    }
    void IInteractable.InteractionUpdate(Player player) {

    }
    void IInteractable.InteractionFixedUpdate(Player player) {

    }
    void IInteractable.InteractionEnd(Player player) {
        print("Interakcja koniec");

    }
}
