using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
        stages[m_stage].gameObject.SetActive(true);
        stages[m_stage].alpha = 0;


        if (m_stage < stages.Length - 1) {
            sequence.Append(stages[m_stage].DOFade(1, 1f));
        } else {
            button.GetComponent<Button>().enabled = false;
            sequence.Append(stages[m_stage].DOFade(1, 1f));
            sequence.AppendCallback(() => {
                SceneManager.LoadScene(1);
            });
        }
        sequence.Play();
    }

}
