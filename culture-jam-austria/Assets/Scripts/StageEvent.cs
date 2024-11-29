using UnityEngine;
using UnityEngine.Events;

public class StageEvent : StageBehaviour {
    [SerializeField] private StageSettings m_whichStage;

    [SerializeField] private UnityEvent m_onStageStarted;
    [SerializeField] private UnityEvent m_onStageEnded;


    protected override void OnStageChanged(int s) {
        if (s == m_whichStage.stageIndex) {
            m_onStageStarted.Invoke();
        } else if (s == m_whichStage.stageIndex + 1) {
            m_onStageEnded.Invoke();
        }
    }
}
