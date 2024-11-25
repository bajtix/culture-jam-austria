using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PointerController : MonoBehaviour {
	public Transform pointA; // Reference to the starting point
	public Transform pointB; // Reference to the ending point
	public RectTransform safeZone; // Reference to the safe zone RectTransform
	public float moveSpeed = 100f; // Speed of the pointer movement
	[SerializeField] private GameObject m_progressBar;
	[SerializeField] private TextMeshProUGUI m_scoreBar;
	public bool CraftingSuccess = false;

	private float m_direction = 1f; // 1 for moving towards B, -1 for moving towards A
	private RectTransform pointerTransform;
	private Vector3 targetPosition;
	private float score = 0;

	void Start() {
		pointerTransform = GetComponent<RectTransform>();
		targetPosition = pointB.position;
	}

	void Update() {
		m_scoreBar.text = score.ToString();
		// Move the pointer towards the target position
		pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPosition, moveSpeed * Time.deltaTime);

		// Change direction if the pointer reaches one of the points
		if (Vector3.Distance(pointerTransform.position, pointA.position) < 0.1f) {
			targetPosition = pointB.position;
			m_direction = 1f;
		} else if (Vector3.Distance(pointerTransform.position, pointB.position) < 0.1f) {
			targetPosition = pointA.position;
			m_direction = -1f;
		}

		// Check for input
		if (Input.GetKeyDown(KeyCode.Space)) {
			CheckSuccess();
		}

		if (score > 10) {
			CraftingSuccess = true;
			m_progressBar.SetActive(false);
		}
	}

	void CheckSuccess() {
		// Check if the pointer is within the safe zone
		if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, pointerTransform.position, null)) {
			score++;
		} else {
			score = 0;
		}
	}
}
