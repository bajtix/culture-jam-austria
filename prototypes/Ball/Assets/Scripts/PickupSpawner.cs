using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
	public GameObject pickup;
	public GameObject ground;
	private Vector3 m_spawnCenter;
	private Vector3 m_spawnSize;
	private float m_spawnTime = 4f;
	public Transform parent;
	private void Start() {
		Renderer groundRenderer = ground.GetComponent<Renderer>();
		m_spawnCenter = groundRenderer.bounds.center;
		m_spawnSize = groundRenderer.bounds.size;
		InvokeRepeating("PickupSpawn", 0f, m_spawnTime);
	}
	private void Update() { 
    }
	private void PickupSpawn() {
		Spawn(pickup);
	}
	public void Spawn(GameObject type) {
		Vector3 random = new Vector3(
			Random.Range((m_spawnCenter.x - (m_spawnSize.x / 2)),(m_spawnCenter.x + m_spawnSize.x / 2)),
			1,
			Random.Range(m_spawnCenter.z - (m_spawnSize.z / 2),(m_spawnCenter.z + m_spawnSize.z / 2))
			);
		Instantiate(type, random, Quaternion.identity, parent);
	}
}
