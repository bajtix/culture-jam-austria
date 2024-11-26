using System;
using UnityEngine;
using UnityEngine.UI;

public class DiggingSystem : Interactable {
	[SerializeField] private GameObject m_canvasDiggingBar;
	[SerializeField] private Image m_diggingProgressFill;
	[SerializeField] private Vector3 m_screenPosition;
	[SerializeField] private Vector3 m_worldPosition;
	[SerializeField] private float m_fillSpeed = 0.2f;
	private float m_percentageDig = 0;
	private bool m_dugUp = false;

	public override string Tooltip => "Dig up the body";

	public float DigUp() {
		return m_percentageDig;
	}

	private void ShowDiggingProgressBar(bool status) {
		m_canvasDiggingBar.SetActive(status);
	}

	public override bool CanInteract(Player player) => !m_dugUp;
	public override bool CanStopInteraction(Player player) => true;
	public override bool InteractionOver(Player player) => m_dugUp;
	public override void InteractionStart(Player player) {
		Debug.Log("Interaction start");
		ShowDiggingProgressBar(true);
		// Game.Player.Controller.AddViewModifier("diggingView", transform.position, 0.6f);
		Game.Player.Controller.AddSpeedModifier("diggingSpeed", 0f);
	}

	public override void InteractionUpdate(Player player) {
		m_screenPosition = Input.mousePosition;
		m_screenPosition.z = Camera.main.nearClipPlane +1;
		m_worldPosition = Camera.main.ScreenToWorldPoint(m_screenPosition);

		var mouseMovement = Game.Input.Player.Look.ReadValue<Vector2>().normalized;
		var leftMouseClick = Game.Input.UI.Click.IsPressed();
		if (leftMouseClick && mouseMovement.y < 0 && m_worldPosition.y > 0.5 & m_worldPosition.y < 1.5) {
			m_diggingProgressFill.fillAmount += Math.Abs(mouseMovement.y * m_fillSpeed * Time.deltaTime);
			m_percentageDig = m_diggingProgressFill.fillAmount;
		}
		if (m_diggingProgressFill.fillAmount == 1) {
			m_dugUp = true;
		}
	}

	public override void InteractionEnd(Player player) {
		Debug.Log("The body was dug up");
		Debug.Log("Interaction end");
		// player.Controller.RemoveViewModifier("diggingView");
		player.Controller.RemoveSpeedModifier("diggingSpeed");
		ShowDiggingProgressBar(false);
		Game.GiveBelt();
	}
}
