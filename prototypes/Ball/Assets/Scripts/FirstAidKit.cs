using Unity.VisualScripting;
using UnityEngine;

public class FirstAidKit : MonoBehaviour {
	public PlayerHealth playerHealth;

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			playerHealth.AddHealth(0.3f);
			Destroy(gameObject);
		}
	}
}
