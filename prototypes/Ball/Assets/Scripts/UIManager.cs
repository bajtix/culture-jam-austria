using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

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
		SetHealth(1f);
	}

	public void SetCountText(int coins) {
		coinsText.text = coins.ToString();
	}

	public void SetHealth(float value) {
		healthBar.GetComponent<Slider>().value = value;
	}
}
