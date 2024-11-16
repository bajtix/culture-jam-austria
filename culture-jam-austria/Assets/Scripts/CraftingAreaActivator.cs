using UnityEngine;

public class CraftingAreaActivator : MonoBehaviour {

	[SerializeField] private GameObject m_progressBar;
	[SerializeField] private PlayerController m_playerController;
	[SerializeField] private PointerController m_pointerController;

	void Start()
    {
        
    }


    void Update()
    {
		if (m_pointerController.CraftingSuccess) {
			m_playerController.RemoveSpeedModifier("Stop");
		}
    }

	private void OnTriggerEnter(Collider other) {
		m_progressBar.SetActive(true);
		m_playerController.AddSpeedModifier("Stop", 0f);
	}
}
