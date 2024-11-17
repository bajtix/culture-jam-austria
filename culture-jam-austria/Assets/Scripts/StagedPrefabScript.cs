using UnityEngine;

public class StagedPrefabScript : MonoBehaviour {
	[SerializeField] private GameObject[] m_variants;
	[SerializeField] private Vector3 m_childPositionDelta;
	private int m_stage = 0;
	private int m_targetStage = 0;
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
		foreach(var variant in m_variants) {
			variant.SetActive(false);
		}
		m_variants[m_stage].SetActive(true);
	}

	public void SetTargetStage(int target) {
		m_targetStage = target;
		if (m_targetStage > m_variants.Length - 1) {
			m_targetStage = m_variants.Length - 1;
		}
	}

	private void UpdateStage() {
		m_stage = m_targetStage;
		SpawnChild();
	}

	private bool CanSwap() {
		return Vector3.Dot(m_player.forward, (transform.position - m_player.position).normalized) < 0;
	}
}
