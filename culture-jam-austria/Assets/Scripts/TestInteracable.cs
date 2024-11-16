using UnityEngine;

public class TestInteracable : MonoBehaviour, IInteractable {
    string IInteractable.Tooltip => "tinga the tanga";

    void IInteractable.HighlightBegin(Player player) {
        transform.localScale = Vector3.one * 0.4f;
    }
    bool IInteractable.CanInteract(Player player) => throw new System.NotImplementedException();
    bool IInteractable.CanStopInteraction(Player player) => throw new System.NotImplementedException();
    void IInteractable.HighlightEnd(Player player) {
        transform.localScale = Vector3.one * 0.3f;
    }
    void IInteractable.InteractionEnd(Player player) => throw new System.NotImplementedException();
    void IInteractable.InteractionFixedUpdate(Player player) => throw new System.NotImplementedException();
    bool IInteractable.InteractionOver(Player player) => throw new System.NotImplementedException();
    void IInteractable.InteractionUpdate(Player player) => throw new System.NotImplementedException();
    void IInteractable.InteractionStart(Player player) => throw new System.NotImplementedException();
}
