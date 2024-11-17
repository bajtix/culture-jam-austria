using UnityEngine;

public class POIScript : MonoBehaviour {
	[SerializeField] private float m_maxTimer;
	[SerializeField] private float m_tickUpSpeed;
	private float m_timer;
	private bool m_inTrigger;
	[Header("IMPORTANT: time stamps have to be sorted in descending order")]
	[SerializeField] private float[] m_timeStamps;
	[SerializeField] private StagedPrefabScript[] m_stagedPrefabs;
	[SerializeField] private float[] m_blizzardLevels;
	private int m_stage = 0;

	private void Start() {
		m_timer = m_maxTimer;
	}

	private void Update() {
		if (m_inTrigger) {
			m_timer -= Time.deltaTime;

			if (m_stage < m_timeStamps.Length) {
				if (m_timer < m_timeStamps[m_stage]) {
					m_stage++;
					if (m_stage < m_blizzardLevels.Length) {
						Game.Blizzard.SetIntensity(m_blizzardLevels[m_stage]);
					}
					Debug.Log("stage = " + m_stage);
				}
			}

			if (m_timer <= 0) {
				TimerFinished();
			}

		} else {
			m_timer += Time.deltaTime * m_tickUpSpeed;
			if (m_timer > m_maxTimer) {
				m_timer = m_maxTimer;
			}
		}
		UpdateStagedPrefabs();
	}

	private void TimerFinished() {
		Debug.Log("Timer finished");
	}


	private void OnTriggerEnter(Collider other) {
		if (other.tag != "Player") {
			return;
		}
		Debug.Log("Player entered " + gameObject.name + " POI");
		m_inTrigger = true;
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag != "Player") {
			return;
		}
		Debug.Log("Player exited " + gameObject.name + " POI");
		m_inTrigger = false;
	}

	private void UpdateStagedPrefabs() {
		foreach (var stagedPrefab in m_stagedPrefabs) {
			stagedPrefab.SetTargetStage(m_stage);
		}
	}
}
