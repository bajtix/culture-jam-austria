using UnityEngine;

public class CraftingAreaActivator : MonoBehaviour, IInteractable {

	[SerializeField] private GameObject m_progressBar;
	[SerializeField] private PlayerController m_playerController;
	[SerializeField] private PointerController m_pointerController;

	string IInteractable.Tooltip => "Craft";

	void Start()
    {
        
    }


 

	void IInteractable.HighlightBegin(Player player) {

	}
	void IInteractable.HighlightEnd(Player player) {

	}
	bool IInteractable.CanInteract(Player player) => true;
	bool IInteractable.CanStopInteraction(Player player) => true;
	bool IInteractable.InteractionOver(Player player) {};
	void IInteractable.InteractionStart(Player player) => throw new System.NotImplementedException();
	void IInteractable.InteractionUpdate(Player player) {

	}
	void IInteractable.InteractionFixedUpdate(Player player) => throw new System.NotImplementedException();
	void IInteractable.InteractionEnd(Player player) => throw new System.NotImplementedException();
}
