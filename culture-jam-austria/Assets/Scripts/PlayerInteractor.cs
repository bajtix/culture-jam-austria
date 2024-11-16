using System;
using NaughtyAttributes;
using UnityEngine;

public class PlayerInteractor : PlayerComponent {
    [BoxGroup("Components")][SerializeField] private Camera m_camera;
    [SerializeField] private float m_maxDistance = 1;
    [SerializeField] private LayerMask m_interactionMask = int.MaxValue;

    private void OnEnable() {
        print("test");
        Game.Input.Player.Interact.performed += (_) => Interact();
        Game.Input.Player.Interact.started += (_) => Debug.Log("started called");
        Game.Input.Player.Interact.performed += (_) => Debug.Log("performed called");
    }

    private void Interact() {
        Debug.Log("i1");
        if (!Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out var hit, m_maxDistance, m_interactionMask)) return;
        print("i2");
        var it = hit.collider.GetComponent<IInteractable>();
        if (it == null) return;

        it.StartInteracting(Player);
    }

    private void FixedUpdate() {
    }

}