using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	public GameState state;

	public static event Action<GameState> OnStateChange;

	private void Awake() {
//		if (instance != null) {
//			Destroy(gameObject);
//		} else {
			instance = this;
//			DontDestroyOnLoad(gameObject);
//		}
	}

	private void Start() {
		UpdateGameState(GameState.Menu);
	}

	public void UpdateGameState(GameState newState) {
		state = newState;

		switch (newState) {
			case GameState.Menu:
				HandleMenu();
				break;
			case GameState.Play:
				HandlePlay();
				break;
			case GameState.GameOver:
				HandleGameOver();
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
		}
		OnStateChange?.Invoke(newState);
	}

	private void HandleMenu() {
		Time.timeScale = 0f;
	}

	private void HandlePlay() {
		Time.timeScale = 1f;
	}

	private void HandleGameOver() {
		Time.timeScale = 0f;
	}
}

public enum GameState {
	Menu,
	Play,
	GameOver,
	Restart,
}
