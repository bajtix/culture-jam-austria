using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Tweens;

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
        var tween = new GraphicAlphaTween() {
            from = 1,
            to = 0,
            duration = 2
        };
        m_fader.gameObject.AddTween(tween);
        m_winPanel.SetActive(false);
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

    public void Dead() {
        m_deathPanel.SetActive(true);
    }

    public void Win() {
        m_winPanel.SetActive(true);
        var tween = new GraphicAlphaTween() {
            from = 0,
            to = 1,
            duration = 2,
            delay = 3
        };
        m_fader.gameObject.AddTween(tween);
    }
}