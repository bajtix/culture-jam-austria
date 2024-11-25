using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DiggingSystem : Interactable {
	[SerializeField] private GameObject m_canvasDiggingBar;
	[SerializeField] private Image m_diggingProgressFill;
	[SerializeField] private float m_fillSpeed = 0.1f;
	[SerializeField] private GameObject[] m_snowElementsList;
	private bool m_diggingActivate = false;
	private bool m_dugUp = false;
	public override string Tooltip => "Digging system";

	private void Start() {
		m_snowElementsList = GameObject.FindGameObjectsWithTag("Snow");
	}
	private void ShowDiggingProgressBar(bool status) {
		m_canvasDiggingBar.SetActive(status);
	}
	private void CheckFillAmount(int numberTab) {
		if (m_diggingProgressFill.fillAmount >= 1.0f / m_snowElementsList.LongLength * (numberTab + 1)) {
			m_snowElementsList[numberTab].SetActive(false);
		}
	}

	public override bool CanInteract(Player player) {
		return !m_diggingActivate;
	}
	public override bool CanStopInteraction(Player player) {
		return true;
	}
	public override bool InteractionOver(Player player) => false;
	public override void InteractionStart(Player player) {
		print("Interaction start");
		ShowDiggingProgressBar(true);
		Game.Player.Controller.AddViewModifier("digging", transform.position, 0.6f);
	}
	public override void InteractionUpdate(Player player) {
		var mouseMovement = Game.Input.Player.Look.ReadValue<Vector2>().normalized;
		var leftMouseClick = Game.Input.UI.Click.IsPressed();
		if (leftMouseClick) {
			m_diggingProgressFill.fillAmount += Math.Abs(mouseMovement.y * m_fillSpeed * Time.deltaTime);
			for (int i = 0; i <= m_snowElementsList.Length; i++) {
				CheckFillAmount(i);
			}
		}
		if (m_diggingProgressFill.fillAmount == 1) {
			ShowDiggingProgressBar(false);
			player.Controller.RemoveViewModifier("digging");
			m_dugUp = true;
			Game.GiveBelt();
		}
	}

	public override void InteractionEnd(Player player) {
		player.Controller.RemoveViewModifier("digging");
		ShowDiggingProgressBar(false);
	}
}
