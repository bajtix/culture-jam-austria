using UnityEngine;

public class TearingPlanks : MonoBehaviour, IInteractable {
	[SerializeField] private GameObject plank;
	private int moveCount = 0;

	void DestroyPlanks(){
		plank.SetActive(false);
	}
	public string Tooltip => "Tearing Planks";

	public bool CanInteract(Player player) => true;
	public bool CanStopInteraction(Player player) => true;
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
			if(mouseMovement.x > 40 || mouseMovement.y > 20) {
				moveCount++;
				if(moveCount >= 10){
					DestroyPlanks();
				}
			}
		}

	}
	public void InteractionFixedUpdate(Player player) {
	}
	public bool InteractionOver(Player player) {
		return plank.activeSelf==false;
	}
	public void InteractionStart(Player player) {

	}

}
