using UnityEngine;

public class PlayerStatusInformer : PlayerComponent {
    private void Update() {
        //  stamina bar
        Game.UI.PlayerStatus.StaminaSetVisible(Player.Controller.Stamina < 1);
        Game.UI.PlayerStatus.StaminaSet(Player.Controller.Stamina);
        Game.UI.PlayerStatus.StaminaSetTired(Player.Controller.IsTired);
    }
}
