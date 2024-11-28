using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class CraftingAreaActivator : Interactable {

	[SerializeField] private GameObject m_progressBar;
	[SerializeField] private TextMeshProUGUI m_scoreBar;
	[SerializeField] private GameObject m_pointer;
	[SerializeField] private RectTransform m_pointA;
	[SerializeField] private RectTransform m_pointB;
	[SerializeField] private RectTransform m_safeZone;
	[SerializeField] private float m_moveSpeed = 100f;
	private bool m_craftingSuccess = false;

	private float m_direction = 1f;
	private RectTransform m_pointerTransform;
	private Vector3 m_targetPosition;
	private float m_score = 0;

	public override string Tooltip => "Nail the belt to the skis";

	private void CheckSuccess() {
		if (RectTransformUtility.RectangleContainsScreenPoint(m_safeZone, m_pointerTransform.position, null)) {
			m_score++;
		} else {
			m_score = 0;
		}
	}

	public override bool CanInteract(Player player) => !m_craftingSuccess;
	public override bool CanStopInteraction(Player player) => true;
	public override bool InteractionOver(Player player) => m_craftingSuccess;
	public override void InteractionStart(Player player) {
		Debug.Log("->> Interaction START <<--");

		m_pointerTransform = m_pointer.GetComponent<RectTransform>();
		m_targetPosition = m_pointA.position;
		m_progressBar.SetActive(true);

		Game.Player.Controller.AddSpeedModifier("CraftingSpeed", 0f);
		Game.Player.Controller.AddViewModifier("CraftingView", GetComponent<BoxCollider>().transform.position, 0.5f);
	}

	public override void InteractionUpdate(Player player) {
		m_scoreBar.text = "Correct hammer blows: " + m_score.ToString();

		m_pointerTransform.position = Vector3.MoveTowards(m_pointerTransform.position, m_targetPosition, m_moveSpeed * Time.deltaTime);

		if (Vector3.Distance(m_pointerTransform.position, m_pointA.position) < 0.1f) {
			Debug.Log(m_pointerTransform.position);
			m_targetPosition = m_pointB.position;
			m_direction = 1f;
		} else if (Vector3.Distance(m_pointerTransform.position, m_pointB.position) < 0.1f) {
			m_targetPosition = m_pointA.position;
			m_direction = -1f;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			CheckSuccess();
		}

		if (m_score > 10) {
			m_craftingSuccess = true;
		}
	}

	public override void InteractionEnd(Player player) {
		Game.Player.Controller.RemoveSpeedModifier("CraftingSpeed");
		Game.Player.Controller.RemoveViewModifier("CraftingView");
		m_progressBar.SetActive(false);

		Debug.Log("-->> Interaction END <<--");
	}
}
