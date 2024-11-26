using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class StopwatchHelper : MonoBehaviour {

    private float m_time = 0;
    private float m_lap = 0;
    private int m_lc = 0;

    private void OnEnable() {
        Game.Input.Player.Jump.performed += ToggleWatch;
    }


    private void OnDisable() {
        Game.Input.Player.Jump.performed -= ToggleWatch;
    }

    private void ToggleWatch(InputAction.CallbackContext context) {
        Game.UI.Debug.JournalLog($"<b>{m_lc}</b> l: {m_lap:0.0}s\tt:{m_time:0.0}s");
        m_lap = 0;
        m_lc++;
    }

    private void FixedUpdate() {
        m_time += Time.fixedDeltaTime;
        m_lap += Time.fixedDeltaTime;
        Game.UI.Debug.SetStatusVar("swatch", m_time);
    }
}
