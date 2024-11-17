using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CraftingAreaActivator : MonoBehaviour, IInteractable {

	[SerializeField] private GameObject m_progressBar;
	[SerializeField] private RectTransform m_pointer;
	[SerializeField] private TextMeshProUGUI m_scoreBar;

	public RectTransform pointA;
	public RectTransform pointB;
	public RectTransform safeZone;
	public float moveSpeed = 100f;
	public bool CraftingSuccess = false;

	public float m_direction = 1f;
	private Vector3 targetPosition;
	private float score = 0;
	void CheckSuccess() {
		if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, m_pointer.position, null)) {
			score++;
		} else {
			score = 0;
		}
	}

	string IInteractable.Tooltip => "Craft";

	void IInteractable.HighlightBegin(Player player) {
		
	}
	void IInteractable.HighlightEnd(Player player) {
	}
	bool IInteractable.CanInteract(Player player) => true;
	bool IInteractable.CanStopInteraction(Player player) => true;
	bool IInteractable.InteractionOver(Player player) => false;
	void IInteractable.InteractionStart(Player player) {
		targetPosition = pointB.position;
		player.Controller.AddSpeedModifier("Stop", 0f);
		m_progressBar.SetActive(true);
	}
	void IInteractable.InteractionUpdate(Player player) {
		m_scoreBar.text = score.ToString();

		m_pointer.position = Vector3.MoveTowards(m_pointer.position, targetPosition, moveSpeed * Time.deltaTime);

		if (Vector3.Distance(m_pointer.position, pointA.position) < 0.1f) {
			targetPosition = pointB.position;
			m_direction = 1f;
		} else if (Vector3.Distance(m_pointer.position, pointB.position) < 0.1f) {
			targetPosition = pointA.position;
			m_direction = -1f;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			CheckSuccess();
		}

		if (score > 10) {
			CraftingSuccess = true;
		}
	}
	void IInteractable.InteractionFixedUpdate(Player player) {

	}
	void IInteractable.InteractionEnd(Player player) {
		m_progressBar.SetActive(false);
		player.Controller.RemoveSpeedModifier("Stop");
	}

}
