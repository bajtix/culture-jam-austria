using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI m_interactionTooltip;
    [SerializeField] private TextMeshProUGUI m_statusText;
    [SerializeField] private Slider m_snowstormStrengthSlider;
    [SerializeField] private UIPlayerStatus m_uiPlayerStatus;

    public UIPlayerStatus PlayerStatus => m_uiPlayerStatus;

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

    public void SetSnowstormStrength(float t) {
        m_snowstormStrengthSlider.value = t;
    }

    internal void SetStatusText(string v) {
        m_statusText.text = v;
    }
}