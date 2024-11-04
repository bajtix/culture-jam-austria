using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	[Header("Scripts")]
	public PlayerController playerController;

	[Header("Coins")]
	public TextMeshProUGUI coinsText;

	[Header("Helth")]
	public GameObject healthBar;

	[Header("Canvas")]
	public GameObject gameOverCanvas;
	public GameObject menuCanvas;

	private void Awake() {
		GameManager.OnStateChange += GameManager_OnStateChange;
	}

	private void OnDestroy() {
		GameManager.OnStateChange -= GameManager_OnStateChange;
	}

	private void GameManager_OnStateChange(GameState state) {
		menuCanvas.SetActive(state == GameState.Menu);

		gameOverCanvas.SetActive(state == GameState.GameOver);
	}

	private void Start() {
		SetCountText(0);
		ChangedHealth();
	}

	public void SetCountText(int coins) {
		coinsText.text = coins.ToString();
	}

	public void ChangedHealth() {
		playerController.health = Mathf.Clamp(playerController.health, 0, 1);
		healthBar.GetComponent<Slider>().value = playerController.health;
	}
}
