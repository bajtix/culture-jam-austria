using System;
using UnityEngine;
using UnityEngine.UI;

public class DiggingSystem : Interactable {
	[SerializeField] private GameObject m_diggingCanvas;
	[SerializeField] private Image m_diggingProgressFill;
	[SerializeField] private float m_fillSpeed = 0.2f;
	[SerializeField] private float m_howOften = 0.1f;

	private Vector3 m_mouseScreenPosition;
	private Vector3 m_mouseWorldPosition;

	private float m_indexDig = 1;
	public bool dugUp = false;

	public override string Tooltip => "Dig up the body";

	private void DigUp() {
		//removing a piece of snow
	}

	public override bool CanInteract(Player player) => !dugUp;
	public override bool CanStopInteraction(Player player) => true;
	public override bool InteractionOver(Player player) => dugUp;
	public override void InteractionStart(Player player) {
		Debug.Log("->> Digging - interaction start <<--");

		m_diggingCanvas.SetActive(true);
		player.Controller.AddSpeedModifier("diggingSpeed", 0f);
		// Game.Player.Controller.AddViewModifier("diggingView", transform.position, 1f);
	}

	public override void InteractionUpdate(Player player) {
		m_mouseScreenPosition = Input.mousePosition;
		m_mouseScreenPosition.z = player.Camera.nearClipPlane + 1;
		m_mouseWorldPosition = player.Camera.ScreenToWorldPoint(m_mouseScreenPosition);

		var mouseMovement = Game.Input.Player.Look.ReadValue<Vector2>().normalized;
		bool leftMouseClick = Game.Input.UI.Click.IsPressed();

		if (leftMouseClick && mouseMovement.y < 0 && m_mouseWorldPosition.y > 0.6 & m_mouseWorldPosition.y < 1.5) {
			m_diggingProgressFill.fillAmount += Math.Abs(mouseMovement.y) * m_fillSpeed * 0.006f;
			if (m_diggingProgressFill.fillAmount > (m_howOften * m_indexDig)) {
				DigUp();
				Debug.Log("You dug up a piece of snow: " + m_indexDig);
				m_indexDig++;
			}
		}

		if (m_diggingProgressFill.fillAmount == 1) {
			dugUp = true;
		}
	}

	public override void InteractionEnd(Player player) {
		if (dugUp) {
			Debug.Log("The body was dug up");
		}

		// player.Controller.RemoveViewModifier("diggingView");
		player.Controller.RemoveSpeedModifier("diggingSpeed");
		m_diggingCanvas.SetActive(false);

		Debug.Log("-->> Digging - interaction end <<--");
	}
}
