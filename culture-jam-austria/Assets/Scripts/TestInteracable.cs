using UnityEngine;

public class TestInteracable : Interactable {
    public override string Tooltip => "tinga the tanga";

    public override void InteractionEnd(Player player) {
        player.Controller.RemoveSpeedModifier("test");
        player.Controller.RemoveViewModifier("test");
        transform.localScale = Vector3.one * 0.4f;
    }

    public override void InteractionFixedUpdate(Player player) {
        transform.localScale = Vector3.one * Random.Range(0.2f, 0.4f);
    }
    public override void InteractionStart(Player player) {
        player.Controller.AddSpeedModifier("test", 0);
        player.Controller.AddViewModifier("test", transform.position, 0.8f);
    }
}
