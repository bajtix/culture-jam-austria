using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Camera m_camera;
    [SerializeField] private CharacterController m_controller;
    [SerializeField] private float m_walkSpeed = 3;
    [SerializeField] private float m_runSpeed = 6;
    [SerializeField] private float m_mouselook = 4;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        var inputs = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

        var mouseY = Input.GetAxis("Mouse Y");
        var mouseX = Input.GetAxis("Mouse X");

        transform.Rotate(0, mouseX * m_mouselook, 0);
        m_camera.transform.Rotate(Vector3.right, -mouseY * m_mouselook);

        var delta = transform.TransformDirection(inputs.normalized);
        m_controller.Move((Input.GetKey(KeyCode.LeftShift) ? m_runSpeed : m_walkSpeed) * delta * Time.deltaTime + Physics.gravity * 0.5f);
    }
}
