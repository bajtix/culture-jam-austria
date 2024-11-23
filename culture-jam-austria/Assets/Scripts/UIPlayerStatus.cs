using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatus : MonoBehaviour {
    [SerializeField] private Slider m_stamina;

    [SerializeField] private Color m_staminaNormalColor, m_staminaTiredColor;


    public void StaminaSetTired(bool t) {
        m_stamina.fillRect.GetComponent<Image>().color = t ? m_staminaTiredColor : m_staminaNormalColor;
    }

    public void StaminaSetVisible(bool t) {
        m_stamina.gameObject.SetActive(t);
    }

    public void StaminaSet(float t) {
        m_stamina.value = t;
    }
}