using UnityEngine;
using Nrjwolf.Tools.AttachAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private float m_speed = 4;
    [SerializeField][GetComponent] private CharacterController m_controller;
    [SerializeField][GetComponentInChildren] private Camera m_camera;

    private Dictionary<string, float> m_speedModifiers = new Dictionary<string, float>();
    private Vector3 m_desiredLookPoint;

    private float m_yaw, m_pitch;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void AddModifier(string name, float value) {
        if (!IsModifier(name)) {
            m_speedModifiers.Add(name, value);
        }
    }

    public bool IsModifier(string name) {
        return m_speedModifiers.ContainsKey(name);
    }

    public void RemoveModifier(string name) {
        if (IsModifier(name)) {
            m_speedModifiers.Remove(name);
        } else {
            Debug.LogWarning("removing nonexisting speed modifier " + name);
        }
    }

    private void FixedUpdate() {
        var input = Game.Input.Player.Move.ReadValue<Vector2>();
        float speedModifier = m_speedModifiers.Count == 0 ? 1 : m_speedModifiers.Aggregate((a, b) => new("", a.Value * b.Value)).Value;
        float speed = m_speed * speedModifier;

        var movement = transform.TransformDirection(new Vector3(
            input.x, 0, input.y
        )).normalized * speed * Time.fixedDeltaTime;

        if (!m_controller.isGrounded)
            movement.y += Physics.gravity.y * Time.fixedDeltaTime;
        else movement.y = 0;

        m_controller.Move(movement);
    }

    private void Update() {
        var mouseMovement = Game.Input.Player.Look.ReadValue<Vector2>();
        m_yaw += mouseMovement.x;
        m_pitch -= mouseMovement.y;
        if (m_yaw > 360) m_yaw = m_yaw % 360;
        if (m_yaw < 0) m_yaw = 360 - m_yaw;

        if (m_pitch > 85) m_pitch = 85;
        if (m_pitch < -85) m_pitch = -85;	

        m_camera.transform.localRotation = Quaternion.Euler(m_pitch, 0, 0);
        transform.rotation = Quaternion.Euler(0, m_yaw, 0);
    }

}
