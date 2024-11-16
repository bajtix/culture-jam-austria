using UnityEngine;

public class TestInteracable : MonoBehaviour, IInteractable {
    public string Tooltip { get => "chuj"; }
    public void BeginHighlight(Player player) => throw new System.NotImplementedException();
    public void EndHighlight(Player player) => throw new System.NotImplementedException();
    public bool StartInteracting(Player player) {
        player.Controller.AddViewModifier(nameof(TestInteracable), transform.position, 0.8f);
        print("kutas");
        return true;
    }
}
