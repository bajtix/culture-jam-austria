using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;


/*
    BROADCASTS:
    OnStageChanged
    OnPlayerDied
    OnPlayerEnteredSafety
    OnPlayerExitedSafety
*/

public class GameController : MonoBehaviour {
    [ReadOnly][SerializeField] private bool m_playerSafe;
    [ReadOnly][SerializeField] private bool m_stormStrengthening, m_monsterHunting;
    [ReadOnly][SerializeField] private float m_insideTime, m_outsideTime, m_stormTime;
    [ReadOnly] private int m_currentStageIndex = -1;

    [SerializeField] private StageSettings[] m_stages;


    [BoxGroup("Events")] public UnityEvent<int> onStageChanged;
    [BoxGroup("Events")] public UnityEvent onPlayerEnteredSafety;
    [BoxGroup("Events")] public UnityEvent onPlayerExitedSafety;
    [BoxGroup("Events")] public UnityEvent onPlayerDied;
    [BoxGroup("Events")] public UnityEvent onGameLost;
    [BoxGroup("Events")] public UnityEvent onGameWon;


    public StageSettings CurrentStage => m_stages[m_currentStageIndex];

    public bool IsPlayerSafe => m_playerSafe;
    public bool IsOutsideDeadly => m_stormTime >= CurrentStage.deadlyStormTime;
    public bool IsMonsterHuntPossible => m_stormTime >= CurrentStage.huntingStartStormTime;
    public bool IsMonsterHunting => m_monsterHunting;

    public float StormDeadlyPercent => Mathf.Clamp01(m_stormTime / CurrentStage.deadlyStormTime);
    public float StormHuntingPercent => Mathf.Clamp01(m_stormTime / CurrentStage.huntingStartStormTime);
    public float StormEndHuntingPercent => Mathf.Clamp01(m_stormTime / CurrentStage.huntingEndStormTime);

    private void Awake() {
        for (int i = 0; i < m_stages.Length; i++) {
            m_stages[i].stageIndex = i;
        }
    }

    private void Start() {
        SetStage(0);
    }

    public void SetSafe(bool to) {
        if (to != m_playerSafe) {
            if (to) OnEnterSafezone();
            else OnExitSafezone();
        }

        m_playerSafe = to;
    }

    private void OnEnterSafezone() {
        m_playerSafe = true;
    }

    private void OnExitSafezone() {
        m_playerSafe = false;
    }

    private void FixedUpdate() {
        if (m_playerSafe) {
            m_insideTime += Time.fixedDeltaTime;
            // if the monster hunt has not been started, the storm shuld recede after a given amount of time
            if (m_insideTime > CurrentStage.stormDirectionChangeTime) {
                m_outsideTime = 0;

                if (!m_monsterHunting) {
                    m_stormStrengthening = false;
                } else if (m_stormTime >= CurrentStage.huntingEndStormTime) { // monster orgasm
                    AdvanceStage();
                    m_monsterHunting = false;
                    m_stormStrengthening = false;
                }
            }


        } else {
            m_outsideTime += Time.fixedDeltaTime;

            // if we stay outside long enough, the storm will start getting stronger
            if (m_outsideTime >= CurrentStage.stormDirectionChangeTime) {
                m_insideTime = 0;
                m_stormStrengthening = true;
            }

            if (m_stormTime > CurrentStage.huntingStartStormTime) {
                m_monsterHunting = true;
            }

            if (m_stormTime >= CurrentStage.deadlyStormTime) {
                KillPlayer();
            }
        }


        // change the stormtime 
        if (m_stormStrengthening) {
            m_stormTime += Time.fixedDeltaTime;
        } else if (m_stormTime > CurrentStage.stormMinTime) {
            m_stormTime -= Time.fixedDeltaTime * CurrentStage.stormRecessionRate;
        }

        m_stormTime = Mathf.Clamp(m_stormTime, 0, CurrentStage.huntingEndStormTime);

        // visuals

        Game.Blizzard.SetIntensity(Mathf.Clamp01(m_stormTime / CurrentStage.deadlyStormTime));

        if (Game.UI.Debug) {
            Game.UI.Debug.SetProgressBar(Mathf.Clamp01(m_stormTime / CurrentStage.deadlyStormTime));
            Game.UI.Debug.SetStatusVar("Safe", m_playerSafe);
            Game.UI.Debug.SetStatusVar("Monster Hunt", m_monsterHunting);
            Game.UI.Debug.SetStatusVar("Outside Deadly", IsOutsideDeadly);
        }
    }

    private void AdvanceStage() {
        Debug.Log("NEXT STAGE");
        if (m_currentStageIndex + 1 < m_stages.Length)
            SetStage(m_currentStageIndex + 1);
        else GameOver();
    }

    private void SetStage(int to) {
        if (m_currentStageIndex == to) return;
        m_currentStageIndex = to;

        Debug.Log("Stage change to " + to);
        if (Game.UI.Debug) {
            Game.UI.Debug.JournalLog("Stage change to " + to);
            Game.UI.Debug.SetStatusVar("stage", m_currentStageIndex);
        }

        onStageChanged.Invoke(m_currentStageIndex);
    }

    private void GameOver() {
        Debug.Log("ITS JOEVER");
        onGameLost.Invoke();
    }


    private void KillPlayer() {
        Debug.Log("PLAYER DIED");
        onPlayerDied.Invoke();
    }
}
