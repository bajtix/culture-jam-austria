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

	private int m_score = 0;
	private bool m_plankIsCut = false;
	private bool m_isA = true;
	private float m_timer = 0f;
	private float m_timeLimit = 2f;
	private bool m_promptActive = false;
	private float m_nextPromptDelay = 0f;
	private float m_sliderMinValue = 0f;
	private float m_sliderMaxValue = 1f;
	private float m_sliderStep = 0.002f;
	private float m_sliderCenterValue = 0.5f;
	private float m_sliderTolerance = 0.1f;
	private float m_timeOutsideRange = 0f;

	public override string Tooltip => "Cut Plank";

	private void Start() {
		m_isA = UnityEngine.Random.Range(0, 2) == 0;
		UpdatePrompt();
		m_slider.value = m_sliderCenterValue;
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
			}
			else {
				DecreaseSliderValue();
			}
			CheckSliderPosition();
		}
	}



	public override bool CanInteract(Player player) => !m_plankIsCut;
	public override bool CanStopInteraction(Player player) => true;
	public override bool InteractionOver(Player player) => m_plankIsCut;
	public override void InteractionStart(Player player) {
		print("Start interaction");
		m_sawMinigame.SetActive(true);
		player.Controller.AddSpeedModifier("cutting", 0);
		m_timer = 0f;
		UpdatePrompt();
		Game.Player.Controller.AddViewModifier("CUTTINGView", transform.position, 1f);
	}

	public override void InteractionUpdate(Player player) {
		if (m_score == 20) {
			print("Plank was cut");
			m_plankIsCut = true;
			m_sawMinigame.SetActive(false);
		}
	}

	public override void InteractionEnd(Player player) {
		print("Stop interaction");
		player.Controller.RemoveSpeedModifier("cutting");
		m_timeLimit = 2f;
		player.Controller.RemoveViewModifier("CUTTINGView");
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
			m_timer = 0f;
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
			m_timer = 0f;
		}
	}

	private void UpdateScoreText() {
		if (m_scoreText != null) {
			m_scoreText.text = $"Score: {m_score}";
		}
	}

	private void UpdatePrompt() {
		m_promptActive = false;
		m_buttonAImage.enabled = false;
		m_buttonDImage.enabled = false;
		float progress = Mathf.InverseLerp(0, 20, m_score);
		m_nextPromptDelay = Mathf.Lerp(0.3f, 0.7f, progress);
		m_timeLimit = Mathf.Lerp(0.7f, 0.2f, progress);
	}

	private void OnTimeExpired() {
		print("Time expired!");
		m_score = Mathf.Max(0, m_score - 1);
		UpdateScoreText();
		m_buttonAImage.enabled = false;
		m_buttonDImage.enabled = false;
		m_promptActive = false;
	}
	private void ActivatePrompt() {
		m_promptActive = true;
		m_isA = UnityEngine.Random.Range(0, 2) == 0;
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
		if (m_slider.value < m_sliderMaxValue) {
			m_slider.value += m_sliderStep;
		}
	}
	private void DecreaseSliderValue() {
		if (m_slider.value > m_sliderMinValue) {
			m_slider.value -= m_sliderStep;
		}
	}
	private void CheckSliderPosition() {
		if (Mathf.Abs(m_slider.value - m_sliderCenterValue) <= m_sliderTolerance) {
			m_timeOutsideRange = 0f;
		} else {
			m_timeOutsideRange += Time.deltaTime;
			if (m_timeOutsideRange >= 1f) {
				m_score = Mathf.Max(0, m_score - 1);
				UpdateScoreText();
				m_timeOutsideRange = 0f;
			}
		}
	}
}
