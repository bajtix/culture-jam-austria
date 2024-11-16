using UnityEngine;

public class POIScript : MonoBehaviour
{
	[SerializeField] private float m_maxTimer;
	[SerializeField] private float m_tickUpSpeed;

	private float m_timer;

	private bool m_inTrigger;
	[Header("IMPORTANT: time stamps have to be sorted in descending order")]
	[SerializeField] private float[] m_timeStamps;

	[SerializeField] private stagedPrefabScript[] m_stagedPrefabs;

	private int stage = 0;

	private void Start() {
		m_timer = m_maxTimer;
	}

	private void Update() {
		if (m_inTrigger) {
			m_timer -= Time.deltaTime;

			if (stage < m_timeStamps.Length) {
				if (m_timer < m_timeStamps[stage]) {
					stage++;
					Debug.Log("stage = " + stage);
				}
			}

			if(m_timer <= 0) {
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
		if (other.tag == "Player") {
			Debug.Log("Player entered " + gameObject.name + " POI");
			m_inTrigger = true;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			Debug.Log("Player exited " + gameObject.name + " POI");
			m_inTrigger = false;
		}
	}

	private void UpdateStagedPrefabs() {
		foreach (var stagedPrefab in m_stagedPrefabs) {
			stagedPrefab.SetTargetStage(stage);
		}
	}
}
