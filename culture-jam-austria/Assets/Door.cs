using System;
using UnityEngine;

public class Door : Interactable {

    public override string Tooltip => m_isLocked ? "Locked" : m_isOpen ? "Close" : "Open";

    [SerializeField] private float m_closedAngle;
    [SerializeField] private float m_openAngle;
    [SerializeField] private float m_openSpeed;
    [SerializeField] private Collider m_collider;

    [SerializeField] private PlayerVoiceline m_lockedVoiceline;
    private bool m_isOpen;
    private bool m_isLocked;

    private float m_angle;


    public override bool CanInteract(Player player) => true;
    public override void InteractionEnd(Player player) { }
    public override bool InteractionOver(Player player) => true;
    public override void InteractionStart(Player player) {
        if (!m_isLocked) SetOpen(!m_isOpen);
        else if (m_lockedVoiceline != null) player.Cutscene.PlayVoiceline(m_lockedVoiceline);
    }

    public void SetLocked(bool isLocked) {
        m_isLocked = isLocked;
        if (m_isLocked) SetOpen(false);
    }

    public void SetOpen(bool v) {
        if (m_isOpen == v) return;
        m_isOpen = v;
    }

    private void Update() {
        m_angle = Mathf.Lerp(m_angle, m_isOpen ? m_openAngle : m_closedAngle, Time.deltaTime * m_openSpeed);
        transform.localRotation = Quaternion.Euler(-90, m_angle, 0);

        m_collider.enabled = Mathf.Abs(m_angle - (m_isOpen ? m_openAngle : m_closedAngle)) < 1f;
    }
}
