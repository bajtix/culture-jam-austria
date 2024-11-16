using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : PlayerComponent {
    [BoxGroup("Components")][SerializeField] private CharacterController m_controller;
    [BoxGroup("Components")][SerializeField] private Camera m_camera;
    public Camera Camera => m_camera;

    [SerializeField] private float m_speed = 4;
    [SerializeField] private float m_lookPitchLimit = 85;

    private Dictionary<string, float> m_speedModifiers = new Dictionary<string, float>();
    private KeyValuePair<string, (float, Vector3)>? m_viewModifier;
    private float m_yaw, m_pitch;


    private void Start() {
        if (Game.Input == null) Debug.LogError("No game input found!");

        Cursor.lockState = CursorLockMode.Locked;
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
        if (m_yaw > 360) m_yaw %= 360;
        if (m_yaw < 0) m_yaw = 360 - m_yaw;

        if (m_pitch > m_lookPitchLimit) m_pitch = m_lookPitchLimit;
        if (m_pitch < -m_lookPitchLimit) m_pitch = -m_lookPitchLimit; // TODO: fix that


        m_camera.transform.localRotation = Quaternion.AngleAxis(m_pitch, Vector3.right);
        transform.rotation = Quaternion.AngleAxis(m_yaw, Vector3.up);

        if (m_viewModifier != null) {
            var desiredLook = Quaternion.LookRotation(m_viewModifier.Value.Value.Item2 - m_camera.transform.position, Vector3.up);
            m_camera.transform.rotation = Quaternion.Lerp(m_camera.transform.rotation, desiredLook, m_viewModifier.Value.Value.Item1);
        }
    }

    public void AddSpeedModifier(string name, float value) {
        if (!IsSpeedModifier(name)) {
            m_speedModifiers.Add(name, value);
        } else {
            m_speedModifiers[name] = value;
        }
    }

    public bool IsSpeedModifier(string name) {
        return m_speedModifiers.ContainsKey(name);
    }

    public void RemoveSpeedModifier(string name) {
        if (IsSpeedModifier(name)) {
            m_speedModifiers.Remove(name);
        } else {
            Debug.LogWarning("removing nonexisting speed modifier " + name);
        }
    }

    public void AddViewModifier(string name, Vector3 focus, float strength) {
        if (!IsViewModifier(name) && m_viewModifier != null) {
            Debug.LogWarning($"overwriting a view modifier (current = {m_viewModifier.Value.Key}, new = {name})");
            m_viewModifier = new KeyValuePair<string, (float, Vector3)>(name, (strength, focus));
        } else {
            m_viewModifier = new KeyValuePair<string, (float, Vector3)>(name, (strength, focus));
        }
    }

    public bool IsViewModifier(string name) {
        return m_viewModifier != null && m_viewModifier.Value.Key == name;
    }

    public void RemoveViewModifier(string name) {
        if (IsViewModifier(name)) {
            m_viewModifier = null;
        } else {
            Debug.LogWarning("removing nonexisting view modifier " + name);
        }
    }

}
