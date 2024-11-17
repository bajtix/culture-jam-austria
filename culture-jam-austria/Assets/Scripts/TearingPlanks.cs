using UnityEngine;

public class TearingPlanks : MonoBehaviour, IInteractable {
	[SerializeField] private GameObject plank;
	void DestroyPlanks(){
		Destroy(plank);
	}
	public string Tooltip => "Tearing Planks";

	public bool CanInteract(Player player) => true;
	public bool CanStopInteraction(Player player) => false;
	public void HighlightBegin(Player player) {

	}
	public void HighlightEnd(Player player) {

	}
	public void InteractionEnd(Player player) {

	}
	public void InteractionUpdate(Player player) {
		var mouseMovement = Game.Input.Player.Look.ReadValue<Vector2>();
		var leftMouseClick = Game.Input.UI.Click.IsPressed();
		if (leftMouseClick) {
			int i = 0;
			do
			if(mouseMovement.x > 15 || mouseMovement.y > 15) {
				DestroyPlanks();
				Debug.Log("position x: " + mouseMovement.x + " || position y: " + mouseMovement.y);
				i++;
			}
			while(i < 5);
		}

	}
	public void InteractionFixedUpdate(Player player) {

	}
	public bool InteractionOver(Player player) => false;
	public void InteractionStart(Player player) {

	}

}
