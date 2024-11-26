using UnityEngine;
using UnityEngine.UI;

public class Beartrapmechanic : Interactable {
    [SerializeField] private GameObject m_defusingcircle;
    [SerializeField] private Slider m_defusingSlider;
    private float m_trapworktime = 5f;
    private bool m_isTrapActivated = false;
    private float m_timeSinceActivated = 0f;
    private bool m_isTrapDefused = false;
    private float m_defuseTime = 3f;
    private float m_timeSinceDefusing = 0f;



    public override string Tooltip => m_isTrapDefused ? "Trap defused" : "Defuse trap";

    private void Start() {
        if (m_defusingSlider != null) {
            m_defusingSlider.value = 0f;
            m_defusingSlider.maxValue = m_defuseTime;
        }
    }

    private void Update() {
        if (m_isTrapActivated && !m_isTrapDefused) {
            m_timeSinceActivated += Time.deltaTime;
            m_defusingSlider.value = m_timeSinceActivated / m_trapworktime * 3;
            if (m_timeSinceActivated >= m_trapworktime) {
                Game.Player.Controller.RemoveSpeedModifier("Stop");
                m_isTrapActivated = false;
                m_timeSinceActivated = 0f;
                m_defusingcircle.SetActive(false);
                if (m_defusingSlider != null) {
                    m_defusingSlider.value = 0f;
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
        m_defusingcircle.SetActive(true);
        if (m_defusingSlider != null) {
            m_defusingSlider.value = 0f;
        }

    }

    private void Trap_effect() {
        m_isTrapActivated = true;
        m_timeSinceActivated = 0f;
        Game.Player.Controller.AddSpeedModifier("Stop", 0f);
    }


    public override bool CanInteract(Player player) => !m_isTrapDefused && !m_isTrapActivated;

    public override bool CanStopInteraction(Player player) => false;

    public override void InteractionStart(Player player) {
        print("Interakcja start");
        m_defusingcircle.SetActive(true);
        player.Controller.AddSpeedModifier("defusing", 0);
        m_timeSinceDefusing = 0f;
        if (m_defusingSlider != null) {
            m_defusingSlider.maxValue = m_defuseTime;
        }

    }
    public override void InteractionUpdate(Player player) {
        m_timeSinceDefusing += Time.deltaTime;
        m_defusingSlider.value = m_timeSinceDefusing / m_defuseTime * 3;
        if (m_timeSinceDefusing >= m_defuseTime) {
            print("Trap defused");
            player.Controller.RemoveSpeedModifier("defusing");
            m_isTrapDefused = true;
            Game.Player.Controller.RemoveSpeedModifier("Stop");
            m_defusingcircle.SetActive(false);
        }
    }

    public override void InteractionEnd(Player player) {
        print("Interakcja koniec");
        player.Controller.RemoveSpeedModifier("defusing");
        m_isTrapDefused = true;
        Game.Player.Controller.RemoveSpeedModifier("Stop");
        if (!m_isTrapDefused) {
            player.Controller.RemoveSpeedModifier("defusing");
            m_defusingcircle.SetActive(false);
        }
        if (m_defusingSlider != null) {
            m_defusingSlider.value = 0f;
        }
    }
}
