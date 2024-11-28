using UnityEngine;

public class Pickupable : Interactable {
    public override string Tooltip => "Pick up";
    [SerializeField] protected float m_weight = 1;
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private Collider m_collider;


    public override void InteractionEnd(Player player) {
        player.Controller.RemoveSpeedModifier("carry");
        m_rigidbody.isKinematic = false;
        m_collider.enabled = true;
    }
    public override void InteractionStart(Player player) {
        player.Controller.AddSpeedModifier("carry", Mathf.Clamp(25f / (m_weight * m_weight), 0.1f, 1f));
        m_rigidbody.isKinematic = true;
        m_collider.enabled = false;
    }
    public override void InteractionUpdate(Player player) {
        transform.position = player.PlayerCamera.transform.position + player.PlayerCamera.transform.forward;
    }

}
