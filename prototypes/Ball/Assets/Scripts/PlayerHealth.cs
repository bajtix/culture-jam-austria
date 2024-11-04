using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	public UIManager uiManager;
	public PlayerController playerController;

	public void AddHealth(float howMuch) {
		playerController.health += howMuch;
		uiManager.ChangedHealth();
	}

	public void SubtractHealth(float howMuch) {
		playerController.health -= howMuch;
		uiManager.ChangedHealth();
	}
}
