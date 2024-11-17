using UnityEngine;
using UnityEngine.UI;

public class SawMinigame : MonoBehaviour, IInteractable {
    [SerializeField] private GameObject Defusingcircle;
    [SerializeField] private Slider defusingSlider;
    private float m_trapworktime = 5f;
    private bool m_isTrapActivated = false;
    private float m_timeSinceActivated = 0f;
    private bool m_isTrapDefused = false;
    private float m_defuseTime = 3f;
    private float m_timeSinceDefusing = 0f;



    string IInteractable.Tooltip => m_isTrapDefused ? "Trap defused" : "Defuse trap";

    private void Start() {
        if (defusingSlider != null) {
            defusingSlider.value = 0f;
            defusingSlider.maxValue = m_defuseTime;
        }
    }

    private void Update() {
        if (m_isTrapActivated && !m_isTrapDefused) {
            m_timeSinceActivated += Time.deltaTime;
            defusingSlider.value = m_timeSinceActivated / m_trapworktime * 3;
            if (m_timeSinceActivated >= m_trapworktime) {
                Game.Player.Controller.RemoveSpeedModifier("Stop");
                m_isTrapActivated = false;
                m_timeSinceActivated = 0f;
                Defusingcircle.SetActive(false);
                if (defusingSlider != null) {
                    defusingSlider.value = 0f;
                }
            }
            if (m_isTrapDefused) {
                return;
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player") || m_isTrapDefused) return;
        Trap_effect();
        Defusingcircle.SetActive(true);
        if (defusingSlider != null) {
            defusingSlider.value = 0f;
        }

    }

    private void Trap_effect() {
        m_isTrapActivated = true;
        m_timeSinceActivated = 0f;
        Game.Player.Controller.AddSpeedModifier("Stop", 0f);
    }

    void IInteractable.HighlightBegin(Player player) {

    }
    void IInteractable.HighlightEnd(Player player) {

    }
    bool IInteractable.CanInteract(Player player) {
        return !m_isTrapDefused && !m_isTrapActivated;
    }
    bool IInteractable.CanStopInteraction(Player player) {
        return false;
    }
    bool IInteractable.InteractionOver(Player player) {
        return false;
    }
    void IInteractable.InteractionStart(Player player) {
        print("Interakcja start");
        Defusingcircle.SetActive(true);
        player.Controller.AddSpeedModifier("defusing", 0);
        m_timeSinceDefusing = 0f;
        if (defusingSlider != null) {
            defusingSlider.maxValue = m_defuseTime;
        }

    }
    void IInteractable.InteractionUpdate(Player player) {
        m_timeSinceDefusing += Time.deltaTime;
        defusingSlider.value = m_timeSinceDefusing / m_defuseTime * 3;
        if (m_timeSinceDefusing >= m_defuseTime) {
            print("Trap defused");
            player.Controller.RemoveSpeedModifier("defusing");
            m_isTrapDefused = true;
            Game.Player.Controller.RemoveSpeedModifier("Stop");
            Defusingcircle.SetActive(false);
        }
    }
    void IInteractable.InteractionFixedUpdate(Player player) {

    }
    void IInteractable.InteractionEnd(Player player) {
        print("Interakcja koniec");
        player.Controller.RemoveSpeedModifier("defusing");
        m_isTrapDefused = true;
        Game.Player.Controller.RemoveSpeedModifier("Stop");
        if (!m_isTrapDefused) {
            player.Controller.RemoveSpeedModifier("defusing");
            Defusingcircle.SetActive(false);
        }
        if (defusingSlider != null) {
            defusingSlider.value = 0f;
        }
    }
}

