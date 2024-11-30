using UnityEngine;

public class BeltClickerMinigame : Interactable {
	[SerializeField] private DiggingSystem m_diggingSystem;

	public override string Tooltip => "Take the belt";

	//public override bool CanInteract(Player player) => m_diggingSystem.dugUp;
	public override bool CanStopInteraction(Player player) => true;
	public override bool InteractionOver(Player player) => false;
	public override void InteractionStart(Player player) {
		Debug.Log("->> Take belt - interaction start <<--");

	}

	public override void InteractionUpdate(Player player) {

	}

	public override void InteractionEnd(Player player) {
		Debug.Log("-->> Take belt - interaction end <<--");
	}
}
