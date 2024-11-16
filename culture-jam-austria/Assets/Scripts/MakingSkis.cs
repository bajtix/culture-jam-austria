using UnityEngine;
using UnityEngine.UI;

public class MakingSkis : MonoBehaviour {
	public GameObject interactionUI; // UI pojawiaj¹ce siê przy stole
	public Animator animator; // Animator obiektu
	private bool m_isPlayerNearby = false; // Czy gracz jest w zasiêgu
	private bool m_isQTEActive = false; // Czy QTE jest aktywne
	public float qteDuration = 2f; // Czas na reakcjê w QTE
	private float m_qteTimer;

	void Start() {
		interactionUI.SetActive(false);
	}

	void Update() {
		if (m_isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // Naciœniêcie E, bêd¹c blisko sto³u
		{
			interactionUI.SetActive(false); // Ukryj UI
			StartQTE(); // Rozpoczêcie QTE
		}

		if (m_isQTEActive) {
			m_qteTimer -= Time.deltaTime;

			if (m_qteTimer <= 0) {
				FailQTE(); // Gracz nie zd¹¿y³ 
			}

			if (Input.GetMouseButtonDown(0)) // Klikniêcie myszk¹
			{
				SuccessQTE();
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) // SprawdŸ, czy gracz jest w triggerze
		{
			m_isPlayerNearby = true;
			interactionUI.SetActive(true); // Poka¿ UI
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")) {
			m_isPlayerNearby = false;
			interactionUI.SetActive(false); // Ukryj UI
		}
	}

	void StartQTE() {
		m_isQTEActive = true;
		m_qteTimer = qteDuration; // Resetuj licznik czasu
		Debug.Log("QTE rozpoczête!");
	}

	void SuccessQTE() {
		m_isQTEActive = false;
		animator.SetTrigger("CraftSkis"); // Wywo³aj animacjê
		Debug.Log("Uda³o siê!");
	}

	void FailQTE() {
		m_isQTEActive = false;
		Debug.Log("Nie uda³o siê.");
		// Mo¿esz dodaæ tutaj dodatkowe konsekwencje
	}
}
