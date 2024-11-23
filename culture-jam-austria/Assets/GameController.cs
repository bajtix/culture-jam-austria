using System;
using NaughtyAttributes;
using UnityEditor;
using UnityEditor.MPE;
using UnityEngine;

public class GameController : MonoBehaviour {

    private void Start() {
        SetMapDamage(0);
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;
        OnEnterHouse();
    }


    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player")) return;
        OnExitHouse();
    }

    // ==

    [SerializeField] private float m_minimalChangeTime = 10;
    [SerializeField] private float m_dangerousTime = 20;
    [SerializeField] private float m_deadlyTime = 40;
    [ReadOnly][SerializeField] private bool m_isPlayerIn = false;
    [ReadOnly][SerializeField] private float m_outsideTime = 0;
    [ReadOnly][SerializeField] private float m_insideTime = 0;
    [ReadOnly][SerializeField] private float m_stormTime = 0;
    [ReadOnly][SerializeField] private bool m_stormApproaching = false;
    [ReadOnly][SerializeField] private int m_stage = 0;
    [SerializeField] private float[] m_stageMinStorms = new float[] { 0, 5, 10 };




    private void SetMapDamage(int to) {
        var pref = FindObjectsByType<StagedPrefabScript>(FindObjectsSortMode.None);

        foreach (var a in pref) {
            a.SetStage(m_stage);
        }
    }

    private bool MonsterHunting => m_stormTime > m_dangerousTime && m_stormApproaching && m_outsideTime > m_minimalChangeTime;

    private void OnEnterHouse() {
        Debug.Log("Back to safety!");
        m_isPlayerIn = true;
        if (MonsterHunting) {
            SetMapDamage(++m_stage);
            m_outsideTime = 0;
        }
    }

    private void OnExitHouse() {
        Debug.Log("Adventure time!");
        m_isPlayerIn = false;
    }

    private void Die() {
        Debug.Log("DEATHH!");
#if UNITY_EDITOR
        EditorApplication.isPaused = true;
#endif
    }

    private void FixedUpdate() {
        if (m_isPlayerIn) {
            if (m_insideTime > m_minimalChangeTime)
                m_outsideTime = 0;
            m_insideTime += Time.fixedDeltaTime;

            if (m_insideTime > m_minimalChangeTime) {
                m_stormApproaching = false;
            }
        } else {
            m_insideTime = 0;
            m_outsideTime += Time.fixedDeltaTime;

            if (m_outsideTime > m_minimalChangeTime) {
                m_stormApproaching = true;
            }

            if (m_stormTime > m_deadlyTime) {
                Die();
            }
        }

        if (m_stormApproaching) {
            if (m_stormTime < m_deadlyTime) m_stormTime += Time.fixedDeltaTime;
        } else {
            if (m_stormTime > m_stageMinStorms[m_stage]) m_stormTime -= Time.fixedDeltaTime;
        }

        Game.UI.SetSnowstormStrength(Mathf.Clamp01(m_stormTime / m_deadlyTime));
        Game.Blizzard.SetIntensity(Mathf.Clamp01(m_stormTime / m_deadlyTime));
        Game.UI.SetStatusText($"{(m_isPlayerIn ? "Safe" : "Unsafe")}\tStage: {m_stage}\tMonster hunting:{MonsterHunting}");
    }
}
