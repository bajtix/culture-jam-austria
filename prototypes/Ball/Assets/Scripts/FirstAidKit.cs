using UnityEngine;

public class FirstAidKit : MonoBehaviour {
	public float regen = 0.1f;
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			other.GetComponent<PlayerHealth>().Modify(regen, gameObject);

			Destroy(gameObject);
		}
	}
}
