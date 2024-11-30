using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : PlayerComponent {
    [SerializeField] private float m_maxDistance = 1;

    private Interactable m_highlighted;
    private Interactable m_interacting;

    private void OnEnable() {
        Game.Input.Player.Interact.performed += Interact;
    }

    private void OnDisable() {
        Game.Input.Player.Interact.performed -= Interact;
    }

    private Interactable GetLookedOn() {
        if (!Physics.Raycast(Player.PlayerCamera.transform.position, Player.PlayerCamera.transform.forward, out var hit, m_maxDistance, Game.Instance.interactionMask)) return null;
        return hit.collider.GetComponent<Interactable>();
    }

    private void FixedUpdate() {
        var it = GetLookedOn();

        if (m_interacting != null) {
            m_interacting.InteractionFixedUpdate(Player);
            return;
        }

        if (it != m_highlighted) {
            if (m_highlighted != null) {
                try {
                    Game.UI.HideInteractionTooltip();
                    m_highlighted.HighlightEnd(Player);
                } catch {
                    Debug.LogError("End highlight threw errors");
                }
            }
            m_highlighted = it;

            if (m_highlighted != null && m_highlighted.CanInteract(Player)) {
                m_highlighted.HighlightBegin(Player);
                Game.UI.SetInteractionTooltip(m_highlighted.Tooltip);
            }
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
            Game.UI.SetInteractionTooltip(m_interacting.Tooltip);
            m_interacting = null;
        }
    }

    private void Interact(InputAction.CallbackContext context) {
        if (m_interacting == null) {
            if (m_highlighted == null) return;
            if (!m_highlighted.CanInteract(Player)) return;
            m_interacting = m_highlighted;
            m_interacting.InteractionStart(Player);
            Game.UI.HideInteractionTooltip();
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