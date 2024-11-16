using UnityEngine;

public class GlobalTimerScript : MonoBehaviour
{
	[SerializeField] private float m_maxTimer;

	private float m_timer;

	public bool canTickDown;

	private void Start() {
		m_timer = m_maxTimer;
	}

	private void Update() {
		if (canTickDown) {
			m_timer -= Time.deltaTime;

			if (m_timer <= 0) {
				GameOver();
			}

		}
	}

	private void GameOver() {
		Debug.Log("game over");
	}
}
