using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SawMinigameScript : Interactable {
	[SerializeField] private GameObject m_sawMinigame;
	[SerializeField] private TMP_Text m_whichButton;
	[SerializeField] private TMP_Text m_scoreText;
	private int m_score = 0;
	private bool m_plankiscut= false;
	private bool m_isA = true;
	public override string Tooltip => "Cut Plank";

	private void Start() {
		m_isA = UnityEngine.Random.Range(0, 2) == 0;
		UpdatePrompt();
	}
	private void Update() {
		if (m_sawMinigame.activeSelf) {
			IsCorrectButton();
		}
	}

	public override bool CanInteract(Player player) => !m_plankiscut;

	public override bool CanStopInteraction(Player player) => false;

	public override bool InteractionOver(Player player) => m_plankiscut;

	public override void InteractionStart(Player player) {
		print("Start interaction");
		m_sawMinigame.SetActive(true);
		player.Controller.AddSpeedModifier("cutting", 0);
		IsCorrectButton();
	}
	public override void InteractionUpdate(Player player) {
		if (m_score == 10) {
			print("plank was cut");
			m_plankiscut = true;
			m_sawMinigame.SetActive(false);
		}
	}

	public override void InteractionEnd(Player player) {
		print("Stop interaction");
		player.Controller.RemoveSpeedModifier("cutting");
	}

	private void IsCorrectButton() {
		if (Input.GetKeyDown(KeyCode.A)) {
			if (m_isA) {
				m_score++;
				print("Correct input!");
				UpdateScoreText();
				UpdatePrompt(); 
			} else {
				m_score = Mathf.Max(0, m_score - 1); 
				print("Wrong input!");
			}
		} else if (Input.GetKeyDown(KeyCode.D)) {
			if (!m_isA) {
				m_score++;
				print("Correct input!");
				UpdateScoreText();
				UpdatePrompt(); 
			} else {
				m_score = Mathf.Max(0, m_score - 1); 
				print("Wrong input!");
			}
		}
	}
	private void UpdateScoreText() {
		if (m_scoreText != null) {
			m_scoreText.text = $"Score: {m_score}";
		}
	}
	private void UpdatePrompt() {
		m_isA = UnityEngine.Random.Range(0, 2) == 0;
		m_whichButton.text = m_isA ? "Press A!" : "Press D!";

	}
}

