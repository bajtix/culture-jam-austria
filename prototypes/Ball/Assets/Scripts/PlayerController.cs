using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
	public float speed = 0;
	public UIManager uiManager;
	private Rigidbody m_rb;

	private int m_points = 0;
	private Vector2 m_movementVector;

	private void Start() {
		m_rb = GetComponent<Rigidbody>();
	}

	private void OnMove(InputValue movementValue) {
		m_movementVector = movementValue.Get<Vector2>();
	}

	private void FixedUpdate() {
		Vector3 movement = new Vector3(m_movementVector.x, 0.0f, m_movementVector.y);
		m_rb.AddForce(movement * speed);
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("Enemy")) {
			uiManager.ShowStatusText("You lost!");
			Destroy(gameObject);
		}
	}
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("PickUp")) {
			Destroy(other.gameObject);
			m_points++;
			uiManager.SetCountText(m_points);
		}
	}
}
