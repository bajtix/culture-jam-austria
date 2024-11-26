using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    public virtual string Tooltip => "Interact";

    /// <summary>
    /// Player looks at interactable
    /// </summary>
    /// <param name="player"></param>
    public virtual void HighlightBegin(Player player) {

    }

    /// <summary>
    /// Player stops looking at interactable
    /// </summary>
    /// <param name="player"></param>
    public virtual void HighlightEnd(Player player) {

    }

    /// <summary>
    /// Can the player start the interaction
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public virtual bool CanInteract(Player player) {
        return true;
    }

    /// <summary>
    /// Can the player manually exit the interaction
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public virtual bool CanStopInteraction(Player player) {
        return true;
    }

    /// <summary>
    /// Is the interaction over
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public virtual bool InteractionOver(Player player) {
        return false;
    }

    /// <summary>
    /// Player starts interacting
    /// </summary>
    /// <param name="player"></param>
    public abstract void InteractionStart(Player player);

    /// <summary>
    /// Called every frame of the interaction
    /// </summary>
    /// <param name="player"></param>
    public virtual void InteractionUpdate(Player player) {

    }

    /// <summary>
    /// Called every fixed timestep of the interaction
    /// </summary>
    /// <param name="player"></param>
    public virtual void InteractionFixedUpdate(Player player) {

    }

    /// <summary>
    /// Called when the interaction ends
    /// </summary>
    /// <param name="player"></param>
    public abstract void InteractionEnd(Player player);

}