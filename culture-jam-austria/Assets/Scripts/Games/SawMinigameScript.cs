using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class SawMinigameScript : Interactable {
	[SerializeField] private GameObject m_canvas;
	[SerializeField] private Image m_buttonAImage;
	[SerializeField] private Image m_buttonDImage;

	[SerializeField] private Slider m_pressureDisplay;
	[SerializeField] private Image m_progressFill;

	[SerializeField] private float m_pressureSensitivity = 1.5f;
	[SerializeField] private float m_timeToClick = 0.2f;
	[SerializeField] private float m_optimalPressureArea = 1.5f;
	[SerializeField] private float m_minTime = 0.5f, m_maxTime = 1f;
	[SerializeField] private float m_requiredSawing = 10;
	[SerializeField] private Transform m_saw;
	[SerializeField] private float m_sawMove = 1.5f, m_sawDown = 3f;
	[SerializeField] private Tatzelcam m_camera;
	[SerializeField] private GameObject m_discardedEnd;
	[SerializeField] private GameObject m_mainEnd;
	[SerializeField] private GameObject m_resultingObject;
	[SerializeField] private Puzzle m_puzzle;
	[SerializeField] private SoundBite m_sound1, m_sound2;

	private bool m_minigamefail = false;
	private bool m_isA = true;
	[SerializeField][ReadOnly] private float m_timer = 0f;
	private float m_nextCut = 1;
	private float m_pressure = 0;
	private float m_progress = 0;

	private bool m_hasRawPlank = false;

	public override string Tooltip => "Cut Plank";


	public void UpdatePuzzleItems() {
		m_hasRawPlank = m_puzzle.Has("rawplank");
		m_mainEnd.SetActive(m_hasRawPlank);
	}

	private void Start() {
		m_isA = Random.Range(0, 2) == 0;
		UpdatePrompt();

		m_discardedEnd.SetActive(false);
		m_mainEnd.SetActive(m_hasRawPlank);
		m_canvas.SetActive(false);
		m_saw.gameObject.SetActive(false);
	}

	public override bool CanInteract(Player player) => m_hasRawPlank;
	public override bool CanStopInteraction(Player player) => true;
	public override bool InteractionOver(Player player) => m_minigamefail || m_progress >= 1;

	public override void InteractionStart(Player player) {
		print("Start interaction");

		player.Controller.AddSpeedModifier("saw", 0f);
		player.Controller.AddViewModifier("saw", transform.position, 1f);
		player.Cutscene.AddCamera("saw", m_camera);

		m_canvas.SetActive(true);
		m_saw.gameObject.SetActive(true);

		m_timer = 0f;
		m_minigamefail = false;
		m_pressure = 0f;
		m_progress = 0f;

	}

	public override void InteractionUpdate(Player player) {
		UpdatePrompt(m_timer > m_nextCut);

		if (Game.Input.Saw.BounceForward.WasPerformedThisFrame()) {

			if (m_isA && m_timer >= m_nextCut
				&& m_timer <= m_nextCut + m_timeToClick
				&& Mathf.Abs(m_pressure - 0.5f) < m_optimalPressureArea
			) {
				CompleteSwitchDirection();
			} else {
				FailDirectionSwitch();
			}
		}

		if (Game.Input.Saw.BounceBackward.WasPerformedThisFrame()) {
			if (!m_isA && m_timer >= m_nextCut
				&& m_timer <= m_nextCut + m_timeToClick
				&& Mathf.Abs(m_pressure - 0.5f) < m_optimalPressureArea
			) {
				CompleteSwitchDirection();
			} else {
				FailDirectionSwitch();
			}
		}

		if (m_timer > m_nextCut + m_timeToClick) {
			FailDirectionSwitch();
		}

		if (Mathf.Abs(m_pressure - 0.5f) < m_optimalPressureArea) {
			m_timer += Time.deltaTime;
			m_progress += Time.deltaTime / m_requiredSawing;
		}


		if (Game.Input.Saw.Press.IsPressed()) {
			m_pressure += Time.deltaTime * m_pressureSensitivity;
		} else {
			m_pressure -= Time.deltaTime * m_pressureSensitivity;
		}

		m_pressure = Mathf.Clamp01(m_pressure);

		m_saw.transform.localPosition =
			(!m_isA ? Mathf.Clamp01(m_timer / m_nextCut) : 1 - Mathf.Clamp01(m_timer / m_nextCut)) * 0.001f * m_sawMove * Vector3.up
			- Vector3.forward * m_progress * 0.001f * m_sawDown;

		if (m_timer > m_nextCut) {
			m_progressFill.fillAmount = 1 - Mathf.Clamp01((m_timer - m_nextCut) / m_timeToClick);
		} else {
			m_progressFill.fillAmount = 0;
		}

		m_pressureDisplay.value = m_pressure;
	}

	private void CompleteSwitchDirection() {
		m_timer = 0;
		if (m_isA) Game.SexMan.Play(m_sound1, transform.position, 0.9f);
		else Game.SexMan.Play(m_sound2, transform.position, 0.9f);
		m_isA = !m_isA;
		m_nextCut = Random.Range(m_minTime, m_maxTime);
	}

	private void FailDirectionSwitch() {
		m_timer = 0;
		m_minigamefail = true;
		Game.Player.Cutscene.Swear();
	}

	[Button("plank")]
	public void AddPlank() {
		m_hasRawPlank = true;
		m_mainEnd.SetActive(m_hasRawPlank);
	}

	public override void InteractionEnd(Player player) {
		print("Stop interaction");

		m_canvas.SetActive(false);
		m_saw.gameObject.SetActive(false);
		player.Controller.RemoveSpeedModifier("saw");
		player.Controller.RemoveViewModifier("saw");
		player.Cutscene.PopCamera("saw");

		if (m_progress >= 1) {
			var sp = Instantiate(m_discardedEnd, m_discardedEnd.transform.parent);
			sp.transform.SetParent(null);
			sp.SetActive(true);
			sp.AddComponent<Rigidbody>();

			m_puzzle.ConsumeAll();

			sp = Instantiate(m_resultingObject, m_resultingObject.transform.parent);
			sp.SetActive(true);
			sp.transform.SetParent(null);
		}

		m_mainEnd.SetActive(m_hasRawPlank);

		m_minigamefail = false;
		m_progressFill.fillAmount = 0f;
		m_timer = 0f;
		m_pressureDisplay.value = 0.5f;
	}

	private void UpdatePrompt(bool promptActive = false) {
		m_buttonAImage.enabled = m_isA && promptActive;
		m_buttonDImage.enabled = !m_isA && promptActive;
	}

}
