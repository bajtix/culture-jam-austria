using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DiggingSystem : MonoBehaviour, IInteractable {
	[SerializeField] private GameObject m_canvasDiggingBar;
	[SerializeField] private Image m_diggingProgressFill;
	[SerializeField] private float m_fillSpeed = 0.1f;
	private GameObject[] m_snowElementsList;
	private bool m_diggingActivate = false;
	private bool m_dugUp = false;
	public string Tooltip => "Digging system";

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
	public void HighlightBegin(Player player) {

	}
	public void HighlightEnd(Player player) {

	}
	public bool CanInteract(Player player) {
		return !m_diggingActivate;
	}
	public bool CanStopInteraction(Player player) {
		return true;
	}
	bool IInteractable.InteractionOver(Player player) {
		return false;
	}
	public void InteractionStart(Player player) {
		print("Interaction start");
		ShowDiggingProgressBar(true);
		Game.Player.Controller.AddViewModifier("digging", transform.position, 0.6f);
	}
	public void InteractionUpdate(Player player) {
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
			m_dugUp = true;
		}
	}
	public void InteractionFixedUpdate(Player player) {

	}
	public void InteractionEnd(Player player) {
		Debug.Log("xd");
		ShowDiggingProgressBar(false);
		player.Controller.RemoveViewModifier("digging");
	}
}
