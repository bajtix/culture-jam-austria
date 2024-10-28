using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
	public Transform player;
	private NavMeshAgent m_navMeshAgent;

	private void Start() {
		m_navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void Update() {
		if (player != null) {
			m_navMeshAgent.SetDestination(player.position);
		}
	}
}
