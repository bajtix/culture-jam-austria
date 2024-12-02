using UnityEngine;

public abstract class StageBehaviour : MonoBehaviour {
    protected virtual void OnEnable() {
        Game.Controller.onStageChanged.AddListener(OnStageChanged);
        Game.Controller.onPlayerEnteredSafety.AddListener(OnPlayerEnteredSafety);
        Game.Controller.onPlayerExitedSafety.AddListener(OnPlayerExitedSafety);
        Game.Controller.onPlayerDied.AddListener(OnPlayerDied);
        Game.Controller.onGameLost.AddListener(OnGameLost);
        Game.Controller.onGameWon.AddListener(OnGameWon);
        Game.Controller.onHuntStarted.AddListener(OnHuntStarted);
    }

    protected virtual void OnDisable() {
        Game.Controller.onStageChanged.RemoveListener(OnStageChanged);
        Game.Controller.onPlayerEnteredSafety.RemoveListener(OnPlayerEnteredSafety);
        Game.Controller.onPlayerExitedSafety.RemoveListener(OnPlayerExitedSafety);
        Game.Controller.onPlayerDied.RemoveListener(OnPlayerDied);
        Game.Controller.onGameLost.RemoveListener(OnGameLost);
        Game.Controller.onGameWon.RemoveListener(OnGameWon);
        Game.Controller.onHuntStarted.RemoveListener(OnHuntStarted);

    }

    protected virtual void OnStageChanged(int s) { }
    protected virtual void OnPlayerEnteredSafety() { }
    protected virtual void OnPlayerExitedSafety() { }
    protected virtual void OnPlayerDied() { }
    protected virtual void OnGameWon() { }
    protected virtual void OnGameLost() { }
    protected virtual void OnHuntStarted() { }

}
