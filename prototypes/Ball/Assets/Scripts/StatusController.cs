using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusController : MonoBehaviour {
	public TextMeshProUGUI coinsText;
	public TextMeshProUGUI statusText;

	private void Start() {
		statusText.enabled = false;
		SetCountText(0);
	}

	public void SetCountText(int coins) {
		coinsText.text = coins.ToString();
	}

	public void ShowStatusText(string status) {
		statusText.enabled = true;
		statusText.text = status;
	}
}
