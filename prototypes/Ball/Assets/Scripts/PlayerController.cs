using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
    public float speed = 0;
    public UIManager uiManager;
    private Rigidbody rb;

    private int points = 0;
    private Vector2 movementVector;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue) {
        movementVector = movementValue.Get<Vector2>();
    }

    void FixedUpdate() {
        Vector3 movement = new Vector3(movementVector.x, 0.0f, movementVector.y);
        rb.AddForce(movement * speed);
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
            points++;
            uiManager.SetCountText(points);
        }
    }
}
