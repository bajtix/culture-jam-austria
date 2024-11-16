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
    private Vector3 m_desiredMovement;

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
        var mod = m_speedModifiers.Count == 0 ? 1 : m_speedModifiers.Aggregate((a, b) => new("", a.Value * b.Value)).Value;
        var speed = m_speed * mod;

        var transformed = transform.TransformDirection(new Vector3(
            input.x, 0, input.y
        )).normalized * speed * Time.fixedDeltaTime;

        m_desiredMovement.x = transformed.x;
        m_desiredMovement.z = transformed.z;

        if (!m_controller.isGrounded)
            m_desiredMovement.y += Physics.gravity.y * Time.fixedDeltaTime;
        else m_desiredMovement.y = 0;

        m_controller.Move(m_desiredMovement);
    }

    private void Update() {
        var mouseMovement = Game.Input.Player.Look.ReadValue<Vector2>();
        m_camera.transform.Rotate(Vector3.right, -mouseMovement.y);
        transform.Rotate(Vector3.up, mouseMovement.x);
    }

}
