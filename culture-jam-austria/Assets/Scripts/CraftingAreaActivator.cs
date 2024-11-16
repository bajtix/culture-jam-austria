using UnityEngine;

public class CraftingAreaActivator : MonoBehaviour {

	[SerializeField] private GameObject ProgressBar;
	[SerializeField] private PlayerController m_playerController;

	void Start()
    {
        
    }


    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other) {
		Instantiate(ProgressBar);
		m_playerController.AddSpeedModifier("Stop", 0f);
	}
}
