using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroShitscene : MonoBehaviour {
    public CanvasGroup button;
    public CanvasGroup[] stages;

    private int m_stage = 0;

    private void Start() {
        button.alpha = 0;

        var sequence = DOTween.Sequence();
        sequence.AppendInterval(2f);
        sequence.Append(button.DOFade(1, 1));
        sequence.Play();
    }

    public void Advance() {
        var sequence = DOTween.Sequence();
        sequence.Append(stages[m_stage].DOFade(0, 1f));
        m_stage++;

        if (m_stage < stages.Length - 1) {
            sequence.Append(stages[m_stage].DOFade(1, 1f));
        } else {
            sequence.Append(stages[m_stage].DOFade(1, 1f));
            sequence.AppendCallback(() => {
                SceneManager.LoadScene(1);
            });
        }
        sequence.Play();
    }

}
