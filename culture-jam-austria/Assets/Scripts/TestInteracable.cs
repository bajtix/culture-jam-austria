using UnityEngine;

public class TestInteracable : MonoBehaviour, IInteractable {
    public string Tooltip { get => "chuj"; }

    string IInteractable.Tooltip => throw new System.NotImplementedException();

    void IInteractable.BeginHighlight(Player player) => throw new System.NotImplementedException();
    bool IInteractable.CanInteract(Player player) => throw new System.NotImplementedException();
    bool IInteractable.CanStopInteraction(Player player) => throw new System.NotImplementedException();
    void IInteractable.EndHighlight(Player player) => throw new System.NotImplementedException();
    void IInteractable.InteractionEnd(Player player) => throw new System.NotImplementedException();
    void IInteractable.InteractionFixedUpdate(Player player) => throw new System.NotImplementedException();
    bool IInteractable.InteractionOver(Player player) => throw new System.NotImplementedException();
    void IInteractable.InteractionUpdate(Player player) => throw new System.NotImplementedException();
    void IInteractable.StartInteracting(Player player) => throw new System.NotImplementedException();
}
