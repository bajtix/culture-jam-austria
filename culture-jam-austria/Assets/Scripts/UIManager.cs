using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI m_interactionTooltip;
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
}