using UnityEngine;

public class TestDiggingSystem : MonoBehaviour {
	public DiggingSystem diggingSystem;
	void Update() {
		if (diggingSystem.DigUp() != 1) {
			Debug.Log("Dug up: "+diggingSystem.DigUp());
		}
	}
}
