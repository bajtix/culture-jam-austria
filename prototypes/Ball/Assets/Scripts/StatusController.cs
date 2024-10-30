using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusController : MonoBehaviour {
	public TextMeshProUGUI coinsText;
	public TextMeshProUGUI statusText;
	public GameObject endScreen;
	public PlayerHealth playerHealth;

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
	public void IsDamage() {
		playerHealth.SubtractHealth(0.1f);
		if (playerHealth.health == 0) {
			IsDie();
		}
	}

	public void IsDie() {
		ShowStatusText("You lost!");
		Time.timeScale = 0f;
		endScreen.SetActive(true);
	}
}
