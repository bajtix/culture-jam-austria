using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CraftingAreaActivator : MonoBehaviour, IInteractable {

	[SerializeField] private GameObject m_progressBar;
	[SerializeField] private TextMeshProUGUI m_scoreBar;

	public Transform pointA;
	public Transform pointB;
	public RectTransform safeZone;
	public float moveSpeed = 100f;
	public bool CraftingSuccess = false;

	private float m_direction = 1f;
	private RectTransform pointerTransform;
	private Vector3 targetPosition;
	private float score = 0;

	string IInteractable.Tooltip => "Craft";

	void Start()
    {
        
    }


	void IInteractable.HighlightBegin(Player player) {
		
	}
	void IInteractable.HighlightEnd(Player player) {

	}
	bool IInteractable.CanInteract(Player player) => true;
	bool IInteractable.CanStopInteraction(Player player) => true;
	bool IInteractable.InteractionOver(Player player) {
		return false;
	}
	void IInteractable.InteractionStart(Player player) {
		pointerTransform = GetComponent<RectTransform>();
		targetPosition = pointB.position;
		Game.Player.Controller.AddSpeedModifier("Stop", 0f);
		m_progressBar.SetActive(true);
	}
	void IInteractable.InteractionUpdate(Player player) {
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
	void IInteractable.InteractionFixedUpdate(Player player) {

	}
	void IInteractable.InteractionEnd(Player player) {
		if (CraftingSuccess) {
		Game.Player.Controller.RemoveSpeedModifier("Stop");
		}
	}
	void CheckSuccess() {
		if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, pointerTransform.position, null)) {
			score++;
		} else {
			score = 0;
		}
	}
}
