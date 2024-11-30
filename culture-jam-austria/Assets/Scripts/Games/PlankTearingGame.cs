using UnityEngine;

public class PlankTearingGame : Interactable {
    [SerializeField] private Transform m_plank;
    [SerializeField] private GameObject m_resultingItem;
    [SerializeField] private float m_timeItTakes;

    private float m_progress = 0;

    private Vector3 m_initialPlankPosition;
    private Quaternion m_initialPlankRotation;

    private void Start() {
        m_initialPlankPosition = m_plank.localPosition;
        m_initialPlankRotation = m_plank.localRotation;

        m_resultingItem.SetActive(false);
        m_plank.gameObject.SetActive(true);
    }

    public override bool CanInteract(Player player) => m_progress < 1;

    public override bool InteractionOver(Player player) => m_progress >= 1;

    public override void InteractionStart(Player player) {
        m_progress = 0;
        player.Controller.AddSpeedModifier("planktearing", 0f);
        player.Controller.AddViewModifier("planktearing", m_initialPlankPosition, 0.8f);
    }

    public override void InteractionUpdate(Player player) {
        m_progress += Time.deltaTime / m_timeItTakes;

        m_plank.localPosition = Vector3.Lerp(m_initialPlankPosition, m_resultingItem.transform.localPosition, m_progress);
        m_plank.localRotation = Quaternion.Lerp(m_initialPlankRotation, m_resultingItem.transform.localRotation, m_progress);

        Game.UI.SetProgress("Tearing plank", m_progress);
    }

    public override void InteractionEnd(Player player) {
        if (m_progress >= 1) {
            m_resultingItem.SetActive(true);
            m_resultingItem.transform.SetParent(null);
            m_plank.gameObject.SetActive(false);
        }
        player.Controller.RemoveSpeedModifier("planktearing");
        player.Controller.RemoveViewModifier("planktearing");
        Game.UI.HideProgress();
    }
}
