using System;
using NaughtyAttributes;
using UnityEngine;

public class PlayerInteractor : PlayerComponent {
    [BoxGroup("Components")][SerializeField] private Camera m_camera;
    [SerializeField] private float m_maxDistance = 1;
    [SerializeField] private LayerMask m_interactionMask = int.MaxValue;

    private void OnEnable() {
        Game.Input.Player.Interact.started += (_) => Interact();
    }

    private void Interact() {
        if (!Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out var hit, m_maxDistance, m_interactionMask)) return;
        var it = hit.collider.GetComponent<IInteractable>();
        print(it);
        if (it == null) return;

        it.StartInteracting(Player);
    }
}