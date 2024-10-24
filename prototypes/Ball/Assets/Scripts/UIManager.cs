using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public TextMeshProUGUI countText;
    public TextMeshProUGUI statusText;

    private void Start() {
        statusText.enabled = false;
        SetCountText(0);
    }

    public void SetCountText(int points) {
        countText.text = "Count: " + points;
    }

    public void ShowStatusText(string status) {
        statusText.enabled = true;
        statusText.text = status;
    }
}