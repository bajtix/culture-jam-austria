using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SawMinigameScript : Interactable {
	[SerializeField] private GameObject m_sawMinigame;
	[SerializeField] private Image m_buttonAImage;
	[SerializeField] private Image m_buttonDImage;
	[SerializeField] private TMP_Text m_scoreText;
	[SerializeField] private Slider m_slider;
	[SerializeField] private Image m_progressFill;

	private bool m_plankIsCut = false;
	private bool m_minigamefail = false;
	private bool m_isA = true;
	private float m_timer = 0f;
	private float m_timeLimit = 2f;
	private bool m_promptActive = false;
	private float m_nextPromptDelay = 0f;

	public override string Tooltip => "Cut Plank";

	private void Start() {
		m_isA = UnityEngine.Random.Range(0, 2) == 0;
		UpdatePrompt();
		m_slider.value = 0.5f;
		if (m_progressFill != null) {
			m_progressFill.fillAmount = 0f;
		}
	}

	private void Update() {
		if (m_sawMinigame.activeSelf) {
			m_timer += Time.deltaTime;

			if (m_promptActive) {
				if (m_timer > m_timeLimit) {
					OnTimeExpired();
				}
				IsCorrectButton();
			} else {
				m_nextPromptDelay -= Time.deltaTime;
				if (m_nextPromptDelay <= 0f) {
					ActivatePrompt();
				}
			}
			if (Input.GetMouseButton(0)) {
				IncreaseSliderValue();
			} else {
				DecreaseSliderValue();
			}

		}
	}

	public override bool CanInteract(Player player) => !m_plankIsCut;
	public override bool CanStopInteraction(Player player) => false;
	public override bool InteractionOver(Player player) => m_plankIsCut || m_minigamefail;
	public override void InteractionStart(Player player) {
		print("Start interaction");
		m_sawMinigame.SetActive(true);
		player.Controller.AddSpeedModifier("sawSpeed", 0f);
		m_timer = 0f;
			m_progressFill.fillAmount = 0f;
		UpdatePrompt();
		Game.Player.Controller.AddViewModifier("sawView", transform.position, 1f);
	}

	public override void InteractionUpdate(Player player) {
		if (m_progressFill.fillAmount >= 1f) {
			Debug.Log("Plank was cut!");
			m_plankIsCut = true;
		}
	}

	public override void InteractionEnd(Player player) {
		print("Stop interaction");
		m_sawMinigame.SetActive(false);
		player.Controller.RemoveSpeedModifier("sawSpeed");
		m_timeLimit = 2f;
		player.Controller.RemoveViewModifier("sawView");
		m_minigamefail = false;
		m_plankIsCut = false;
		m_progressFill.fillAmount = 0f;
		m_timer = 0f;
		m_slider.value = 0.5f;
	}

	private void IsCorrectButton() {
		if (m_slider.value >= 0.4f && m_slider.value <= 0.6f) {
			if (Input.GetKeyDown(KeyCode.A)) {
				if (m_isA) {
					m_progressFill.fillAmount += 0.05f;
					Debug.Log("Correct! Progress: " + m_progressFill.fillAmount);
					UpdatePrompt();
				} else {
					m_minigamefail = true;
				}
				m_timer = 0f;
			} else if (Input.GetKeyDown(KeyCode.D)) {
				if (!m_isA) {
					m_progressFill.fillAmount += 0.05f;
					Debug.Log("Correct! Progress: " + m_progressFill.fillAmount);
					UpdatePrompt();
				} else {
					m_minigamefail = true;
				}
				m_timer = 0f;
			}
		} else {
			Debug.Log("Kurwa trzymaj w podanej wartoœci");
		}
	}

	private void UpdatePrompt() {
		m_promptActive = false;
		m_buttonAImage.enabled = false;
		m_buttonDImage.enabled = false;
		float progress = m_progressFill.fillAmount;
		m_nextPromptDelay = Mathf.Lerp(0.3f, 0.7f, progress);
		m_timeLimit = Mathf.Lerp(0.7f, 0.2f, progress);
		m_isA = !m_isA;
	}

	private void OnTimeExpired() {
		Debug.Log("Time expired!");
		m_progressFill.fillAmount = Mathf.Max(0, m_progressFill.fillAmount - 0.05f);
		m_buttonAImage.enabled = false;
		m_buttonDImage.enabled = false;
		m_promptActive = false;
	}

	private void ActivatePrompt() {
		m_promptActive = true;
		if (m_isA) {
			m_buttonAImage.enabled = true;
			m_buttonDImage.enabled = false;
		} else {
			m_buttonDImage.enabled = true;
			m_buttonAImage.enabled = false;
		}
		m_timer = 0f;
	}

	private void IncreaseSliderValue() {
		if (m_slider.value < 1f) {
			m_slider.value += 0.003f;
		}
	}

	private void DecreaseSliderValue() {
		if (m_slider.value > 0f) {
			m_slider.value -= 0.003f;
		}
	}
}
