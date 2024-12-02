using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class BearTrapManager : StageBehaviour {
	[SerializeField] private Transform[] m_locations;
	[SerializeField] private Transform[] m_bearTraps;

	[SerializeField] private Clawmark m_clawmark;
	[SerializeField] private float m_clawChance;


	// private void Start() {
	// 	Scatter(); gets called anyways!
	// }

	protected override void OnStageChanged(int s) {
		Scatter(s != 0);
	}

	[Button("Scatter")]
	public void Scatter(bool placeClaws = true) {
		Debug.Log("Scattering bear traps");
		int[] usedPos = new int[m_bearTraps.Length];

		for (int i = 0; i < usedPos.Length; i++) {
			usedPos[i] = -1;
		}

		for (int i = 0; i < m_bearTraps.Length; i++) {
			int index = Random.Range(0, m_locations.Length);
			while (usedPos.Contains(index)) {
				index = Random.Range(0, m_locations.Length);
			}
			usedPos[i] = index;

			if (Random.Range(0, 1f) < m_clawChance && placeClaws) try {
					m_clawmark.Place(m_bearTraps[i].position + Vector3.up * 0.2f);
				} catch {
					//ign
				}
			m_bearTraps[i].position = m_locations[index].position;

		}
	}

}
