using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
	[Header("Player Settings")]
	public float health;
	public float speed = 10;

	[Header("Scripts")]
	public UIManager uiManager;
	public PlayerHealth playerHealth;
	public CameraController cameraController;

	private Rigidbody m_rb;
	private int m_coins = 0;
	private Vector2 m_movementVector;

	private void Start() {
		m_rb = GetComponent<Rigidbody>();
	}

	private void Update() {
		if(health == 0) {
			GameManager.instance.UpdateGameState(GameState.GameOver);
		}
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
			playerHealth.SubtractHealth(1f);
		}
	}
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("PickUp")) {
			Destroy(other.gameObject);
			m_coins++;
			uiManager.SetCountText(m_coins);
		}
	}
}
