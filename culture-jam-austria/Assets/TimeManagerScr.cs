using UnityEngine;

public class TimeManagerScr : MonoBehaviour
{
	[SerializeField] private float m_maxTime;
	[SerializeField] private float m_tickUpSpeed;
	[SerializeField] private float m_tickDownSpeed;

	[SerializeField] private float[] m_blizzardStagesTime;
	[SerializeField] private float[] m_blizzardStagesValues;

	[SerializeField] private StagedPrefabScript[] m_stagedPrefabs;

	[SerializeField] private float m_randomEventDelay;
	[SerializeField] private float m_randomEventChance;
	private float m_lastRandomEventTime = 0;

	private float m_time;

	private int m_stage = 0;
	private void Start() {
		m_time = m_maxTime;
	}

	private void Update() {
		if (!Game.IsSafe()) {
			m_time -= m_tickDownSpeed * Time.deltaTime;

			if (m_time <= 0) {
				FindAnyObjectByType<DeathUIScript>().Die();
			}

			int stageIndex = 0;
			for (int i = 0; i < m_blizzardStagesTime.Length; i++) {
				if (m_time < m_blizzardStagesTime[i]) {
					stageIndex = i;
				}
			}
			
			if (stageIndex > m_stage) { m_stage = stageIndex; }
			Debug.Log("not safe, stage = " + m_stage);
			if (m_stage < m_blizzardStagesValues.Length) {
				Game.Blizzard.SetIntensity(m_blizzardStagesValues[m_stage]);
			}

			

			foreach(StagedPrefabScript prefab  in m_stagedPrefabs) {
				prefab.SetTargetStage(stageIndex);
			}

		}
		else {
			m_time += m_tickUpSpeed * Time.deltaTime;

			if (m_time > m_maxTime) {
				m_time = m_maxTime;
			}

			int stageIndex = 0;
			for (int i = 0; i < m_blizzardStagesTime.Length; i++) {
				if (m_time < m_blizzardStagesTime[i]) {
					stageIndex = i;
				}
			}

			if (stageIndex < m_stage) { m_stage = stageIndex; }
			Debug.Log("safe, stage = " + m_stage);

			Game.Blizzard.SetIntensity(0.1f);
		}
	}
	private void FixedUpdate() {
		if (!Game.IsSafe()) {
			if (Time.time - m_lastRandomEventTime > m_randomEventDelay) {
				float num = Random.Range(0, 1000);
				Debug.Log("Event can occur!");
				if(num/1000 < m_randomEventChance) {
					Debug.Log("Event! num = "+num/1000);
					m_stage++;
					m_lastRandomEventTime = Time.time;
				}
			}
		}
	}
}
