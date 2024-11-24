using UnityEngine;

public class doorMechanics : MonoBehaviour, IInteractable
{

	private bool m_closed = true;

	[SerializeField] private Renderer m_renderer;
	[SerializeField] private Collider m_collider;

	string IInteractable.Tooltip => "Door";

	private void Update() {
		m_renderer.enabled = m_closed;
		m_collider.isTrigger = !m_closed;
	}
	void IInteractable.HighlightBegin(Player player) {

	}
	void IInteractable.HighlightEnd(Player player) {

	}
	bool IInteractable.CanInteract(Player player) {
		return true;
	}
	bool IInteractable.CanStopInteraction(Player player) {
		return false;
	}
	bool IInteractable.InteractionOver(Player player) {
		return m_closed;
	}
	void IInteractable.InteractionStart(Player player) {
		print("drzwi start");
		m_closed = !m_closed;
	}
	void IInteractable.InteractionUpdate(Player player) {

	}
	void IInteractable.InteractionFixedUpdate(Player player) {

	}
	void IInteractable.InteractionEnd(Player player) {
		print("drzwi koniec");
	}
}
