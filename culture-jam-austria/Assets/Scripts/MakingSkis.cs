using UnityEngine;
using UnityEngine.UI;

public class MakingSkis : MonoBehaviour {
	public GameObject interactionUI; // UI pojawiaj�ce si� przy stole
	public Animator animator; // Animator obiektu
	private bool m_isPlayerNearby = false; // Czy gracz jest w zasi�gu
	private bool m_isQTEActive = false; // Czy QTE jest aktywne
	public float qteDuration = 2f; // Czas na reakcj� w QTE
	private float m_qteTimer;

	void Start() {
		interactionUI.SetActive(false);
	}

	void Update() {
		if (m_isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // Naci�ni�cie E, b�d�c blisko sto�u
		{
			interactionUI.SetActive(false); // Ukryj UI
			StartQTE(); // Rozpocz�cie QTE
		}

		if (m_isQTEActive) {
			m_qteTimer -= Time.deltaTime;

			if (m_qteTimer <= 0) {
				FailQTE(); // Gracz nie zd��y� 
			}

			if (Input.GetMouseButtonDown(0)) // Klikni�cie myszk�
			{
				SuccessQTE();
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) // Sprawd�, czy gracz jest w triggerze
		{
			m_isPlayerNearby = true;
			interactionUI.SetActive(true); // Poka� UI
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
		Debug.Log("QTE rozpocz�te!");
	}

	void SuccessQTE() {
		m_isQTEActive = false;
		animator.SetTrigger("CraftSkis"); // Wywo�aj animacj�
		Debug.Log("Uda�o si�!");
	}

	void FailQTE() {
		m_isQTEActive = false;
		Debug.Log("Nie uda�o si�.");
		// Mo�esz doda� tutaj dodatkowe konsekwencje
	}
}
