using System;
using NaughtyAttributes;
using UnityEngine;

public class GameController : MonoBehaviour {
    [ReadOnly][SerializeField] private bool m_isSafe;
    [ReadOnly][SerializeField] private bool m_stormStrengthening, m_monsterHunting;
    [ReadOnly][SerializeField] private float m_insideTime, m_outsideTime, m_stormTime;

    [SerializeField] private float m_stormRecessionRate = 2;


    [InfoBox("How long does it take for the storm to start receeding/proceeding")]
    [SerializeField] private float m_minimalStormDirectionChangeTime;

    [InfoBox("When the storm reaches this level, the monster hunt starts - there is no coming back, it WILL progress to the next stage")]
    [SerializeField] private float m_huntingStartStormTime = 20;

    [InfoBox("When the storm reaches this level, the player cannot survive outside of the shelter")]
    [SerializeField] private float m_deadlyStormTime = 40;

    [InfoBox("When the storm reaches this level, we advance to the next stage and allow the storm to recede.")]
    [SerializeField] private float m_huntingEndStormTime = 50;

    public bool IsPlayerSafe => m_isSafe;
    public bool IsOutsideDeadly => m_stormTime >= m_deadlyStormTime;
    public bool IsMonsterHunting => m_stormTime >= m_huntingStartStormTime;

    public float StormDeadlyPercent => Mathf.Clamp01(m_stormTime / m_deadlyStormTime);
    public float StormHuntingPercent => Mathf.Clamp01(m_stormTime / m_huntingStartStormTime);
    public float StormEndHuntingPercent => Mathf.Clamp01(m_stormTime / m_huntingEndStormTime);

    public void SetSafe(bool to) {
        if (to != m_isSafe) {
            if (to) OnEnterSafezone();
            else OnExitSafezone();
        }

        m_isSafe = to;
    }

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
                } else if (m_stormTime >= m_huntingEndStormTime) { // monster orgasm
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

            if (m_stormTime > m_huntingStartStormTime) {
                m_monsterHunting = true;
            }

            if (m_stormTime >= m_deadlyStormTime) {
                KillPlayer();
            }
        }

        m_stormTime += (m_stormStrengthening ? 1 : -m_stormRecessionRate) * Time.fixedDeltaTime;
        m_stormTime = Mathf.Clamp(m_stormTime, 0, m_huntingEndStormTime);

        Game.Blizzard.SetIntensity(Mathf.Clamp01(m_stormTime / m_deadlyStormTime));

        Game.UI.SetSnowstormStrength(Mathf.Clamp01(m_stormTime / m_deadlyStormTime));
        Game.UI.SetStatusText($"<b>Safe</b> = {m_isSafe}<space=5em><b>Monster hunting</b> = {m_monsterHunting}");
    }

    private void AdvanceStage() {
        Debug.Log("the monster came");
        Debug.Log("NEXT STAGE");
    }
    private void KillPlayer() {
        Debug.Log("players dead");
    }
}
