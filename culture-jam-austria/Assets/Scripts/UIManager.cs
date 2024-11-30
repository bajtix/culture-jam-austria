using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI m_interactionTooltip;
    [SerializeField] private TextMeshProUGUI m_progressText;
    [SerializeField] private GameObject m_progressPanel;
    [SerializeField] private GameObject m_deathPanel;
    [SerializeField] private Image m_progressGraphic;
    [SerializeField] private UIPlayerStatus m_uiPlayerStatus;
    [SerializeField] private UIDebugPane m_uiDebugPane;
    [SerializeField] private UISubtitles m_uiSubtitles;

    public UIPlayerStatus PlayerStatus => m_uiPlayerStatus;
    public UIDebugPane Debug => m_uiDebugPane;
    public UISubtitles Subtitles => m_uiSubtitles;

    private void Start() {
        HideInteractionTooltip();
        HideProgress();
    }

    public void SetInteractionTooltip(string s) {
        m_interactionTooltip.text = s;
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
}