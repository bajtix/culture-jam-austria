using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsUiController : MonoBehaviour {
	public void LoadScene(int x) {
		SceneManager.LoadScene(x);
	}

	public void Exit() {
		Application.Quit();
		Debug.Log("Exit");
	}

	public void Play() {
		GameManager.instance.UpdateGameState(GameState.Play);
	}
}
