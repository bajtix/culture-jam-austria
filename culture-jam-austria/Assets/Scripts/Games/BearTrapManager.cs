using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class BearTrapManager : StageBehaviour {
	[SerializeField] private Transform[] m_locations;
	[SerializeField] private Transform[] m_bearTraps;


	private void Start() {
		Scatter();
	}

	protected override void OnStageChanged(int s) {
		Scatter();
	}

	[Button("Scatter")]
	public void Scatter() {
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

			m_bearTraps[i].position = m_locations[index].position;
		}
	}

}
