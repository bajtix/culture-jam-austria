using UnityEngine;
using UnityEngine.UI;

public class GameBearTrap : Interactable {
    [SerializeField] private Animator m_animator;
    [SerializeField] private GameObject m_bloodDecal;
    [SerializeField] private ParticleSystem m_bloodParticle;

    private float m_trappingTime = 5f;
    private bool m_isTrapActivated = false;
    private float m_timeSinceActivated = 0f;
    private bool m_isTrapDefused = false;
    private float m_defuseTime = 3f;
    private float m_timeSinceDefusing = 0f;

    public override string Tooltip => m_isTrapDefused ? "Trap defused" : "Defuse trap";


    private void Update() {
        if (!m_isTrapActivated) return;

        m_timeSinceActivated += Time.deltaTime;
        Game.UI.SetProgress("Trapped!", m_timeSinceActivated / m_trappingTime);
        m_animator.SetFloat("deftime", 1 - (m_timeSinceActivated / m_trappingTime));

        if (m_timeSinceActivated >= m_trappingTime) {
            StopTrapEffect();
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player") || m_isTrapDefused) return;
        TrapEffect();
    }

    private void TrapEffect() {
        Game.Player.Controller.AddSpeedModifier("Stop", 0f);
        m_isTrapActivated = true;
        m_timeSinceActivated = 0f;
        m_animator.Play("snap");
        Game.Player.Interactor.enabled = false;

        if (m_bloodDecal != null) {
            var go = Instantiate(m_bloodDecal, m_bloodDecal.transform.parent);
            go.SetActive(true);

            var randomVector = Random.insideUnitCircle;
            go.transform.position = m_bloodDecal.transform.position + new Vector3(randomVector.x, 0, randomVector.y);
            go.transform.SetParent(null);
        }

        m_bloodParticle.Play();
    }

    private void StopTrapEffect() {
        Game.Player.Controller.RemoveSpeedModifier("Stop");
        Game.Player.Interactor.enabled = true;
        m_isTrapActivated = false;
        m_timeSinceActivated = 0f;
        Game.UI.HideProgress();
    }


    public override bool CanInteract(Player player) => !m_isTrapDefused && !m_isTrapActivated;

    public override bool CanStopInteraction(Player player) => true;

    public override bool InteractionOver(Player player) => m_timeSinceDefusing >= m_defuseTime;

    public override void InteractionStart(Player player) {
        player.Controller.AddSpeedModifier("defusing", 0);
        m_timeSinceDefusing = 0f;
        m_animator.Play("defusing");
        Game.UI.SetProgress("Defusing trap", 1);
    }

    public override void InteractionUpdate(Player player) {
        m_timeSinceDefusing += Time.deltaTime;
        Game.UI.SetProgress("Defusing trap", m_timeSinceDefusing / m_defuseTime);
        m_animator.SetFloat("deftime", m_timeSinceDefusing / m_defuseTime);
    }

    public override void InteractionEnd(Player player) {
        if (m_timeSinceDefusing >= m_defuseTime) {
            m_isTrapDefused = true;
            print("Trap defused");
            m_timeSinceDefusing = 0;
        } else {
            m_animator.SetFloat("deftime", 0);
        }

        player.Controller.RemoveSpeedModifier("defusing");

        Game.UI.HideProgress();
    }
}
