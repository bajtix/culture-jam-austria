using UnityEngine;


public class TearingPlanks : Interactable {
	[SerializeField] private GameObject m_plank;
	[SerializeField] private GameObject m_canvasInfoTearing;
	private float m_durationShake = 1f;
	private float m_strengthShake = 0.5f;
	private float m_randomnessShake = 1f;
	private int m_vibratoShake = 1;
	private int m_moveCount = 0;

	private void DestroyPlanks() {
		m_plank.SetActive(false);
	}
	public override string Tooltip => "Tear Planks";

	public override bool CanInteract(Player player) => true;
	public override bool CanStopInteraction(Player player) => false;

	public override void InteractionEnd(Player player) {
		//give plank
	}
	public override void InteractionStart(Player player) {
		m_canvasInfoTearing.SetActive(true);
	}
	public override void InteractionUpdate(Player player) {
		var mouseMovement = Game.Input.Player.Look.ReadValue<Vector2>();
		var leftMouseClick = Game.Input.UI.Click.IsPressed();
		if (leftMouseClick) {
			m_canvasInfoTearing.SetActive(false);
			if (mouseMovement.x > 0 || mouseMovement.y > 0) {
			}
			if (mouseMovement.x > 30 || mouseMovement.y > 20) {
				m_moveCount++;
				if (m_moveCount >= 10) {
					DestroyPlanks();
				}
			}
		}

	}
	public override bool InteractionOver(Player player) {
		return m_plank.activeSelf == false;
	}


}
