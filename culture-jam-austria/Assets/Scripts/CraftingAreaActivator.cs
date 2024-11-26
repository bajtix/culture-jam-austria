using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CraftingAreaActivator : Interactable {

	[SerializeField] private GameObject m_progressBar;
	[SerializeField] private TextMeshProUGUI m_scoreBar;

	public Transform pointA;
	public Transform pointB;
	public RectTransform safeZone;
	public float moveSpeed = 100f;
	public bool CraftingSuccess = false;

	private float m_direction = 1f;
	private Transform pointerTransform;
	private Vector3 targetPosition;
	private float score = 0;

	public override string Tooltip => "Craft";

	public override bool CanInteract(Player player) => true;
	public override bool CanStopInteraction(Player player) => true;
	public override bool InteractionOver(Player player) => false;
	public override void InteractionStart(Player player) {
		pointerTransform = GetComponent<Transform>();
		targetPosition = pointB.position;
		Game.Player.Controller.AddSpeedModifier("Stop", 0f);
		m_progressBar.SetActive(true);
	}
	public override void InteractionUpdate(Player player) {
		m_scoreBar.text = score.ToString();

		pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPosition, moveSpeed * Time.deltaTime);

		if (Vector3.Distance(pointerTransform.position, pointA.position) < 0.1f) {
			targetPosition = pointB.position;
			m_direction = 1f;
		} else if (Vector3.Distance(pointerTransform.position, pointB.position) < 0.1f) {
			targetPosition = pointA.position;
			m_direction = -1f;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			CheckSuccess();
		}

		if (score > 10) {
			CraftingSuccess = true;
			m_progressBar.SetActive(false);
		}
	}
	public override void InteractionEnd(Player player) {
		Game.Player.Controller.RemoveSpeedModifier("Stop");
	}
	private void CheckSuccess() {
		if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, pointerTransform.position, null)) {
			score++;
		} else {
			score = 0;
		}
	}
}
