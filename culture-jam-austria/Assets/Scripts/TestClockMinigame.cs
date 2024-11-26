using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestClockMinigame : Interactable {
    [SerializeField] private float m_totalTime;
    [SerializeField] private bool m_canStop;
    [SerializeField] private bool m_saveTime;

    [SerializeField][ReadOnly] private float m_timeLeft;

    [SerializeField] private Slider m_display;
    [SerializeField] private TextMeshProUGUI m_tdisplay;
    [SerializeField] private GameObject m_successObject;

    private void Start() {
        m_timeLeft = m_totalTime;
        m_display.transform.parent.gameObject.SetActive(false);
        m_successObject.SetActive(false);
    }

    public override void InteractionEnd(Player player) {
        m_display.transform.parent.gameObject.SetActive(false);
        player.Controller.RemoveSpeedModifier(gameObject.name);

        if (m_timeLeft <= 0) m_successObject.SetActive(true);
    }
    public override void InteractionStart(Player player) {
        if (!m_saveTime) m_timeLeft = m_totalTime;

        m_display.transform.parent.gameObject.SetActive(true);
        player.Controller.AddSpeedModifier(gameObject.name, 0);
    }

    public override void InteractionFixedUpdate(Player player) {
        m_timeLeft -= Time.fixedDeltaTime;
        m_display.value = m_timeLeft / m_totalTime;
        m_tdisplay?.SetText($"{m_timeLeft:0.0}s ({m_timeLeft * 100 / m_totalTime:0}%)" + (m_canStop ? " [e] to stop" : ""));
        m_display.transform.parent.LookAt(player.Camera.transform.position + player.Camera.transform.forward * 4);
    }

    public override bool CanStopInteraction(Player player) => m_canStop || m_timeLeft <= 0;

    public override bool InteractionOver(Player player) => m_timeLeft <= 0;

    public override bool CanInteract(Player player) => m_timeLeft > 0;
}
