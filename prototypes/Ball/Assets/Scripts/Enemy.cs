using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
	private Transform m_player;
	private NavMeshAgent m_navMeshAgent;

	// damage the player

	private void Start() {
		m_navMeshAgent = GetComponent<NavMeshAgent>();
		GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
		m_player = playerObject.transform;
	}

	private void Update() {
		if (m_player != null) {
			m_navMeshAgent.SetDestination(m_player.position);
		} else {
			Destroy(GameObject.FindGameObjectWithTag("Enemy"));
		}
	}
}
