using UnityEngine;

using DG.Tweening;

public class TearingPlanks : MonoBehaviour, IInteractable {
	[SerializeField] private GameObject plank;
	[SerializeField] private GameObject m_canvasInfoTearing;
	private float m_durationShake = 1f;
	private float m_strengthShake = 0.5f;
	private float m_randomnessShake = 1f;
	private int m_vibratoShake = 1;
	private int m_moveCount = 0;

	void DestroyPlanks() {
		plank.SetActive(false);
	}
	public string Tooltip => "Tear Planks";

	public bool CanInteract(Player player) => true;
	public bool CanStopInteraction(Player player) => false;
	public void HighlightBegin(Player player) {

	}
	public void HighlightEnd(Player player) {

	}
	public void InteractionEnd(Player player) {

	}
	public void InteractionStart(Player player) {
		m_canvasInfoTearing.SetActive(true);
	}
	public void InteractionUpdate(Player player) {
		var mouseMovement = Game.Input.Player.Look.ReadValue<Vector2>();
		var leftMouseClick = Game.Input.UI.Click.IsPressed();
		if (leftMouseClick) {
			m_canvasInfoTearing.SetActive(false);
			if(mouseMovement.x > 0 || mouseMovement.y > 0) {
				transform.DOShakePosition(m_durationShake, m_strengthShake, m_vibratoShake, m_randomnessShake);
			}
			if (mouseMovement.x > 30 || mouseMovement.y > 20) {
				m_moveCount++;
				if (m_moveCount >= 10) {
					DestroyPlanks();
				}
			}
		}

	}
	public void InteractionFixedUpdate(Player player) {
	}
	public bool InteractionOver(Player player) {
		return plank.activeSelf == false;
	}


}
