using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemy;
	private float m_spawnTime = 15f;
	public Transform parent;
	public PickupSpawner pickupSpawner;
	private void Start() {
		InvokeRepeating("EnemySpawn", 0f, m_spawnTime);
	}
	private void EnemySpawn() {
		pickupSpawner.Spawn(enemy);
	}
}
