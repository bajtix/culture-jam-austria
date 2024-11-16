using UnityEngine;

public class StagedPrefabScript : MonoBehaviour {
	[SerializeField] private GameObject[] m_variants;
	[SerializeField] private Vector3 m_childPositionDelta;
	private int m_stage = 0;
	private int m_targetStage = 0;
	private GameObject m_child;
	private Transform m_player;


	private void Start() {
		SpawnChild();
		m_player = Game.Player.transform;
	}

	private void Update() {
		if (m_targetStage > m_stage && CanSwap()) {
			UpdateStage();
		}
	}

	private void SpawnChild() {
		m_child = Instantiate(m_variants[m_stage], transform.position + m_childPositionDelta, transform.rotation);
		m_child.transform.parent = transform;
	}

	public void SetTargetStage(int target) {
		m_targetStage = target;
	}

	private void UpdateStage() {
		m_stage = m_targetStage;
		if (m_stage < m_variants.Length) {
			Destroy(m_child, 0);
			SpawnChild();
		}
	}

	private bool CanSwap() {
		return Vector3.Dot(m_player.forward, (transform.position - m_player.position).normalized) < 0;
	}
}
