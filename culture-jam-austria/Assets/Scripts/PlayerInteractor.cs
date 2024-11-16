using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : PlayerComponent {
    [BoxGroup("Components")][SerializeField] private Camera m_camera;
    [SerializeField] private float m_maxDistance = 1;
    [SerializeField] private LayerMask m_interactionMask = int.MaxValue;

    private IInteractable m_highlighted;
    private IInteractable m_interacting;

    private void OnEnable() {
        Game.Input.Player.Interact.started += Interact;
    }

    private void OnDisable() {
        Game.Input.Player.Interact.started -= Interact;
    }

    private IInteractable GetLookedOn() {
        if (!Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out var hit, m_maxDistance, m_interactionMask)) return null;
        return hit.collider.GetComponent<IInteractable>();
    }

    private void FixedUpdate() {
        var it = GetLookedOn();

        m_interacting?.InteractionFixedUpdate(Player);

        if (it != m_highlighted) {
            if (m_highlighted != null) {
                try {
                    m_highlighted.HighlightEnd(Player);
                } catch {
                    Debug.LogError("End highlight threw errors");
                }
            }
            m_highlighted = it;
            m_highlighted.HighlightBegin(Player);
        }
    }

    private void Update() {
        if (m_interacting == null) return;

        m_interacting.InteractionUpdate(Player);

        if (m_interacting.InteractionOver(Player)) {
            try {
                m_interacting.InteractionEnd(Player);
            } catch {

            }
            m_interacting = null;
        }
    }

    private void Interact(InputAction.CallbackContext context) {
        if (m_interacting == null) {
            if (m_highlighted == null) return;
            if (!m_highlighted.CanInteract(Player)) return;
            m_interacting = m_highlighted;
            m_interacting.InteractionStart(Player);
        } else {
            try {
                if (m_interacting.CanStopInteraction(Player)) {
                    m_interacting.InteractionEnd(Player);
                    m_interacting = null;
                }
            } catch {
                Debug.LogError("Errors; exit interaction");
                m_interacting = null;
            }
        }
    }


}