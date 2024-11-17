using UnityEngine;

public class CraftingAreaActivator : MonoBehaviour {

	[SerializeField] private GameObject ProgressBar;
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
		ProgressBar.SetActive(true);
		m_playerController.AddSpeedModifier("Stop", 0f);
	}
}
