using UnityEngine;

public class BearTrapSpawnScr : MonoBehaviour
{
	[SerializeField] private Transform[] m_locations;
	[SerializeField] private Transform[] m_bearTraps;

	private int m_currentPos;

	private void Start() {
		Scatter();
	}

	private void Update() {
		//if (Game.Input.Player.Jump.WasPressedThisFrame()) {
		//	Scatter();
		//}
	}

	public void Scatter() {
		Debug.Log("Scatter");
		int[] usedPos = new int[m_bearTraps.Length];

		for(int i = 0;i<usedPos.Length;i++) {
			usedPos[i] = -1;
		}


		for(int i = 0;i<m_bearTraps.Length;i++){
			int index = Random.Range(0, m_locations.Length);
			while (InArray(usedPos, index)) {
				index = Random.Range(0, m_locations.Length);
			}
			usedPos[i] = index;

			m_bearTraps[i].position = m_locations[index].position;
		}
	}

	private bool InArray(int[] arr, int val) {
		foreach(int a in arr) {
			if(val == a) {
				return true;
			}
		}
		return false;
	}
}
