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
        Game.Input.Player.Interact.performed += Interact;
    }

    private void OnDisable() {
        Game.Input.Player.Interact.performed -= Interact;
    }

    private IInteractable GetLookedOn() {
        if (!Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out var hit, m_maxDistance, m_interactionMask)) return null;
        return hit.collider.GetComponent<IInteractable>();
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

            if (m_highlighted != null) {
                m_highlighted.HighlightBegin(Player);
                Game.UI.SetInteractionTooltip(Game.Input.Player.Interact.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontOmitDevice) + " to " + m_highlighted.Tooltip);
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