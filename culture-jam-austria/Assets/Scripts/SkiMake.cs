using UnityEngine;

public class SkiMake : MonoBehaviour
{
	[SerializeField] private GameObject m_diggingArea;
	[SerializeField] private GameObject m_clickF;
	[SerializeField] private GameObject m_Player;

	public GameObject snow;

	private void OnTriggerEnter(Collider other) {
		ShowProgressBar();
	}

	void ShowProgressBar() {
		
	}
}
