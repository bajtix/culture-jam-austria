using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : PlayerComponent {
    [BoxGroup("Components")][SerializeField] private CharacterController m_controller;
    [BoxGroup("Components")][SerializeField] private Camera m_camera;
    public Camera Camera => m_camera;

    [BoxGroup("Speed")][SerializeField] private float m_speed = 4;
    [BoxGroup("Speed")][SerializeField] private float m_sprintSpeedMultiplier = 1.4f, m_tiredSpeedMultiplier = 0.9f;
    [BoxGroup("Speed")][SerializeField][Tooltip("Max sprint time in seconds")] private float m_stamina = 4;
    [BoxGroup("Speed")][SerializeField] private float m_staminaRegenMultiplier = 0.5f;
    [BoxGroup("Mouse Look")][SerializeField] private float m_lookPitchLimit = 85;
    [BoxGroup("Mouse Look")][SerializeField] private float m_viewSens = 16, m_interactionSens = .2f;
    [BoxGroup("Mouse Look")][SerializeField] private float m_zeroingSpeed = 2f;

    private float m_currentStamina;
    private bool m_tired = false;

    private Dictionary<string, float> m_speedModifiers = new Dictionary<string, float>();
    private KeyValuePair<string, (float, Vector3)>? m_viewModifier = null;
    private float m_yaw, m_pitch;
    private Vector2 m_mouseOffset;

    [ShowNativeProperty] public float Stamina => m_currentStamina / m_stamina;
    [ShowNativeProperty] public bool IsTired => m_tired;
    [ShowNativeProperty] public bool IsSprinting => Game.Input.Player.Sprint.IsPressed() && !m_tired;
    [ShowNativeProperty] public float MaxSpeed => EvaluateSpeedModifier() * m_speed * EvaluateSprintModifier();
    [ShowNativeProperty] public float Velocity => new Vector2(m_controller.velocity.x, m_controller.velocity.z).magnitude;


    private void Start() {
        if (Game.Input == null) Debug.LogError("No game input found!");

        Cursor.lockState = CursorLockMode.Locked;
    }

    private float EvaluateSpeedModifier() {
        return m_speedModifiers.Count == 0 ? 1 : m_speedModifiers.Aggregate((a, b) => new("", a.Value * b.Value)).Value;
    }

    private float EvaluateSprintModifier() {
        float sprintModifier = 1;

        if (IsSprinting) {
            sprintModifier = m_sprintSpeedMultiplier;
        } else if (m_tired) {
            sprintModifier = m_tiredSpeedMultiplier;
        }

        return sprintModifier;
    }

    private void FixedUpdate() {
        StaminaCalculations();

        var input = Game.Input.Player.Move.ReadValue<Vector2>();

        float speed = m_speed * EvaluateSpeedModifier() * EvaluateSprintModifier();

        var movement = transform.TransformDirection(new Vector3(
            input.x, 0, input.y
        )).normalized * speed * Time.fixedDeltaTime;

        if (!m_controller.isGrounded)
            movement.y += Physics.gravity.y * Time.fixedDeltaTime;
        else movement.y = 0;

        m_controller.Move(movement);
    }

    private void StaminaCalculations() {
        if (IsSprinting) {
            if (m_currentStamina > 0) {
                m_currentStamina -= Time.fixedDeltaTime;
            } else {
                m_tired = true;
            }

        } else {
            if (m_currentStamina < m_stamina) {
                m_currentStamina += Time.fixedDeltaTime * m_staminaRegenMultiplier;
            } else {
                m_tired = false;
            }
        }
        m_currentStamina = Mathf.Clamp(m_currentStamina, 0, m_stamina);
    }

    private void Update() {
        var mouseMovement = Game.Input.Player.Look.ReadValue<Vector2>() * Time.deltaTime;
        if (m_viewModifier == null) {
            m_yaw += mouseMovement.x * m_viewSens;
            m_pitch -= mouseMovement.y * m_viewSens;
            if (m_yaw > 360) m_yaw %= 360;
            if (m_yaw < 0) m_yaw = 360 - m_yaw;

            if (m_pitch > m_lookPitchLimit) m_pitch = m_lookPitchLimit;
            if (m_pitch < -m_lookPitchLimit) m_pitch = -m_lookPitchLimit; // TODO: fix that
        }

        m_camera.transform.localRotation = Quaternion.AngleAxis(m_pitch, Vector3.right);
        transform.rotation = Quaternion.AngleAxis(m_yaw, Vector3.up);

        if (m_viewModifier != null) {
            mouseMovement *= m_interactionSens;
            m_mouseOffset += mouseMovement;

            if (mouseMovement.magnitude < 0.002f) m_mouseOffset = Vector2.Lerp(m_mouseOffset, Vector2.zero, Time.deltaTime * m_zeroingSpeed);
            var destPoint = m_viewModifier.Value.Value.Item2;

            var offsetLook = m_camera.transform.up * m_mouseOffset.y + m_camera.transform.right * m_mouseOffset.x;
            var vec = (destPoint + offsetLook - m_camera.transform.position).normalized;

            var desiredLook = Quaternion.LookRotation(vec, Vector3.up);
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
