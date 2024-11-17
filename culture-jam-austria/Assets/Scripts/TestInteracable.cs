using UnityEngine;

public class TestInteracable : MonoBehaviour, IInteractable {
    string IInteractable.Tooltip => "tinga the tanga";

    void IInteractable.HighlightBegin(Player player) {
        transform.localScale = Vector3.one * 0.4f;
    }
    bool IInteractable.CanInteract(Player player) => true;
    bool IInteractable.CanStopInteraction(Player player) => true;
    void IInteractable.HighlightEnd(Player player) {
        transform.localScale = Vector3.one * 0.3f;
    }
    void IInteractable.InteractionEnd(Player player) {
        player.Controller.RemoveSpeedModifier("test");
        player.Controller.RemoveViewModifier("test");
        transform.localScale = Vector3.one * 0.4f;
    }

    void IInteractable.InteractionFixedUpdate(Player player) {
        transform.localScale = Vector3.one * Random.Range(0.2f, 0.4f);
    }
    bool IInteractable.InteractionOver(Player player) => false;
    void IInteractable.InteractionUpdate(Player player) {

    }
    void IInteractable.InteractionStart(Player player) {
        player.Controller.AddSpeedModifier("test", 0);
        player.Controller.AddViewModifier("test", transform.position, 0.8f);
    }
}
