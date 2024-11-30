using UnityEngine;

public class BeltTearing : Interactable {
    [SerializeField] private Animator m_belt;
    [SerializeField] private GameObject m_resultingItem;
    [SerializeField] private float m_timeItTakes;

    private float m_progress;

    private void Start() {
        m_resultingItem.SetActive(false);
        m_progress = 0;
    }

    public override bool CanInteract(Player player) => m_progress < 1;

    public override bool InteractionOver(Player player) => m_progress >= 1;


    public override void InteractionStart(Player player) {
        m_progress = 0;
        player.Controller.AddSpeedModifier("belttearing", 0f);
        player.Controller.AddViewModifier("belttearing", m_belt.transform.position, 0.8f);
    }

    public override void InteractionUpdate(Player player) {
        m_progress += Time.deltaTime / m_timeItTakes;
        m_belt.SetFloat("progress", m_progress);
        Game.UI.SetProgress("Pulling belt", m_progress);
    }

    public override void InteractionEnd(Player player) {
        if (m_progress >= 1) {
            m_resultingItem.SetActive(true);
            m_resultingItem.transform.SetParent(null);
            m_belt.gameObject.SetActive(false);
        }
        player.Controller.RemoveSpeedModifier("belttearing");
        player.Controller.RemoveViewModifier("belttearing");
        Game.UI.HideProgress();
    }

}
