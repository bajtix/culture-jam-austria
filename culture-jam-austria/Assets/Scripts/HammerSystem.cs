using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class HammerSystem : Interactable {

	[SerializeField] private GameObject m_ui;
	[SerializeField] private RectTransform m_pointer;

	[SerializeField] private Animator m_animator;
	[SerializeField] private float m_moveSpeed = 100f;
	[SerializeField] private int m_requiredHits = 10;
	[SerializeField][ReadOnly] private float m_currentProgress = 0f;
	[SerializeField][ReadOnly] private float m_currentMoveSpeed = 1f;
	[SerializeField] private float m_requiredAccuracy = 0.1f;
	[SerializeField] private float m_acceleration = 1f;
	[SerializeField] private float m_movePixels = 128f;

	[SerializeField] private Tatzelcam m_camera;

	private float m_pointerPosition;
	private bool m_right = true;
	private bool m_hasMissed = false;

	private bool m_hasBelt, m_hasPlank;

	[SerializeField] private GameObject m_plank, m_belt, m_hammer;

	public override string Tooltip => "Nail the belt to the plank";

	private void Start() {
		m_plank.SetActive(m_hasPlank);
		m_belt.SetActive(m_hasBelt);
		m_hammer.SetActive(false);
	}


	public override bool CanInteract(Player player) => m_hasPlank && m_hasBelt;
	public override bool CanStopInteraction(Player player) => true;
	public override bool InteractionOver(Player player) => m_currentProgress >= 1 || m_hasMissed;

	public override void InteractionStart(Player player) {
		player.Controller.AddSpeedModifier("hammer", 0f);
		player.Controller.AddViewModifier("hammer", GetComponent<BoxCollider>().transform.position, 0.5f);
		m_ui.SetActive(true);

		player.Cutscene.AddCamera("hammer", m_camera);

		m_currentMoveSpeed = m_moveSpeed;
		if (m_currentProgress >= 0.5f) {
			m_currentMoveSpeed += m_acceleration * m_requiredHits / 2;
		}

		m_hasMissed = false;
		m_hammer.SetActive(true);
		m_animator.SetBool("active", true);
	}

	public override void InteractionUpdate(Player player) {
		if (m_right) {
			m_pointerPosition += Time.deltaTime * m_currentMoveSpeed;
			if (m_pointerPosition >= 1) m_right = false;
		} else {
			m_pointerPosition -= Time.deltaTime * m_currentMoveSpeed;
			if (m_pointerPosition <= -1) m_right = true;
		}

		m_pointer.anchoredPosition = Vector3.right * (m_pointerPosition * m_movePixels);

		if (Game.Input.Hammer.Hammer.WasPerformedThisFrame()) {
			if (Mathf.Abs(m_pointerPosition) < m_requiredAccuracy)
				Hit();
			else
				Miss();
		}

		m_animator.SetFloat("nail progress", m_currentProgress - 0.01f);
	}

	[Button("plank")]
	public void AddPlank() {
		m_hasPlank = true;
		m_plank.SetActive(m_hasPlank);
		m_belt.SetActive(m_hasBelt);
	}

	[Button("belt")]
	public void AddBelt() {
		m_hasBelt = true;
		m_plank.SetActive(m_hasPlank);
		m_belt.SetActive(m_hasBelt);
	}



	private void Miss() {
		if (m_currentProgress >= 0.5) m_currentProgress = 0.5f;
		else m_currentProgress = 0;
		m_hasMissed = true;
	}

	private void Hit() {
		m_currentProgress += 1f / m_requiredHits;
		m_currentMoveSpeed += m_acceleration;

		m_animator.Play("hammer|beat nail");
	}

	public override void InteractionEnd(Player player) {
		player.Controller.RemoveSpeedModifier("hammer");
		player.Controller.RemoveViewModifier("hammer");

		player.Cutscene.PopCamera("hammer");
		m_ui.SetActive(false);
		m_hammer.SetActive(false);

		if (m_currentProgress >= 1) {
			m_hasPlank = false;
			m_hasBelt = false;
			m_currentProgress = 0;
		}

		m_plank.SetActive(m_hasPlank);
		m_belt.SetActive(m_hasBelt);

		m_animator.SetBool("active", false || m_currentProgress >= 0.5f);
	}
}
