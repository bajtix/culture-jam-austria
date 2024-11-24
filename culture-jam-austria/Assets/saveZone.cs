using UnityEngine;

public class saveZone : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			Game.SetSafe(true);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			Game.SetSafe(false);
		}
	}
}
