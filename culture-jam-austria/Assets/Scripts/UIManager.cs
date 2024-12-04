using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitMotion;

public class UIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI m_interactionTooltip;
    [SerializeField] private TextMeshProUGUI m_progressText;
    [SerializeField] private GameObject m_progressPanel;
    [SerializeField] private GameObject m_deathPanel;
    [SerializeField] private GameObject m_winPanel;

    [SerializeField] private Image m_progressGraphic;
    [SerializeField] private UIPlayerStatus m_uiPlayerStatus;
    [SerializeField] private UIDebugPane m_uiDebugPane;
    [SerializeField] private UISubtitles m_uiSubtitles;

    public UIPlayerStatus PlayerStatus => m_uiPlayerStatus;
    public UIDebugPane Debug => m_uiDebugPane;
    public UISubtitles Subtitles => m_uiSubtitles;

    [SerializeField] private CanvasGroup m_fader;


    private void Start() {
        HideInteractionTooltip();
        HideProgress();
        m_fader.gameObject.SetActive(true);
        LMotion.Create(1f, 0f, 0.4f).Bind((x) => m_fader.alpha = x);
    }

    public void SetInteractionTooltip(string s) {
        m_interactionTooltip.text = "<sprite=0>" + s;
        m_interactionTooltip.gameObject.SetActive(true);
    }

    public void HideInteractionTooltip() {
        m_interactionTooltip.gameObject.SetActive(false);
    }

    public void SetProgress(string title, float progress) {
        m_progressPanel.SetActive(true);
        m_progressText.text = title;
        m_progressGraphic.fillAmount = progress;
    }

    public void HideProgress() {
        m_progressPanel.SetActive(false);
    }

    public void LoadStage(int n) {
        SceneManager.LoadScene(n);
    }

    public void LoadStageAnim(int n) {
        LMotion.Create(0f, 1f, 0.4f)
        .WithOnComplete(() => LoadStage(n))
        .Bind((x) => m_fader.alpha = x);
    }



    public void Dead() {
        m_deathPanel.SetActive(true);
    }

    public void Win() {
        LMotion.Create(0, 1f, 0.4f).WithOnComplete(() => m_winPanel.SetActive(true)).RunWithoutBinding();
        LMotion.Create(0, 1f, 4f).WithDelay(0.4f).WithOnComplete(() => LoadStage(0)).Bind((x) => m_fader.alpha = x);
    }
}