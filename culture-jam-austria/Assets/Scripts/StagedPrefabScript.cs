using NaughtyAttributes;
using UnityEngine;

public class StagedPrefabScript : MonoBehaviour {
	[System.Serializable]
	private struct S {
		public GameObject[] objects;
	}
	[SerializeField] private S[] m_objects;

	private void Start() {

	}

	public void SetStage(int s) {
		foreach (var oar in m_objects) {
			foreach (var o in oar.objects) {
				o.SetActive(false);
			}
		}

		foreach (var o in m_objects[s].objects) {
			o.SetActive(true);
		}
	}
}
