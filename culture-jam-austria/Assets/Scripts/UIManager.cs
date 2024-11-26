using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI m_interactionTooltip;
    [SerializeField] private UIPlayerStatus m_uiPlayerStatus;
    [SerializeField] private UIDebugPane m_uiDebugPane;

    public UIPlayerStatus PlayerStatus => m_uiPlayerStatus;
    public UIDebugPane Debug => m_uiDebugPane;

    private void Start() {
        HideInteractionTooltip();
    }

    public void SetInteractionTooltip(string s) {
        m_interactionTooltip.text = s;
        m_interactionTooltip.gameObject.SetActive(true);
    }

    public void HideInteractionTooltip() {
        m_interactionTooltip.gameObject.SetActive(false);
    }
}