using System;
using NaughtyAttributes;
using UnityEngine;

public class GameController : MonoBehaviour {
    [ReadOnly][SerializeField] private bool m_isSafe;
    [ReadOnly][SerializeField] private bool m_stormStrengthening, m_monsterHunting;
    [ReadOnly][SerializeField] private float m_insideTime, m_outsideTime, m_stormTime;
    [SerializeField] private float m_minimalStormDirectionChangeTime;
    [SerializeField] private float m_dangerousStormTime = 20;
    [SerializeField] private float m_deadlyStormTime = 40;
    [SerializeField] private float m_monsterSatisfiedTime = 50;

    [SerializeField] private DeathUIScript m_death;

    // ---
    private void OnTriggerEnter(Collider c) {
        if (c.CompareTag("Player")) OnEnterSafezone();
    }

    private void OnTriggerExit(Collider c) {
        if (c.CompareTag("Player")) OnExitSafezone();
    }


    // ----
    private void OnEnterSafezone() {
        m_isSafe = true;
    }

    private void OnExitSafezone() {
        m_isSafe = false;
    }

    private void FixedUpdate() {
        if (m_isSafe) {
            m_insideTime += Time.fixedDeltaTime;
            // if the monster hunt has not been started, the storm shuld recede after a given amount of time
            if (m_insideTime > m_minimalStormDirectionChangeTime) {
                m_outsideTime = 0;

                if (!m_monsterHunting) {
                    m_stormStrengthening = false;
                } else if (m_stormTime >= m_monsterSatisfiedTime) { // monster orgasm
                    AdvanceStage();
                    m_monsterHunting = false;
                    m_stormStrengthening = false;
                }
            }


        } else {
            m_outsideTime += Time.fixedDeltaTime;

            // if we stay outside long enough, the storm will start getting stronger
            if (m_outsideTime >= m_minimalStormDirectionChangeTime) {
                m_insideTime = 0;
                m_stormStrengthening = true;
            }

            if (m_stormTime > m_dangerousStormTime) {
                m_monsterHunting = true;
            }

            if (m_stormTime >= m_deadlyStormTime) {
                KillPlayer();
            }
        }

        m_stormTime += (m_stormStrengthening ? 1 : -1) * Time.fixedDeltaTime;
        m_stormTime = Mathf.Clamp(m_stormTime, 0, m_monsterSatisfiedTime);

        Game.Blizzard.SetIntensity(Mathf.Clamp01(m_stormTime / m_deadlyStormTime));
    }

    private void AdvanceStage() {
        Debug.Log("the monster came");
        Debug.Log("NEXT STAGE");
    }
    private void KillPlayer() {
        Debug.Log("players dead");
        m_death.Die();
        Destroy(gameObject);
    }
}
