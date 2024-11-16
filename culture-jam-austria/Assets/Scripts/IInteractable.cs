using UnityEngine;

public interface IInteractable {
    public string Tooltip { get; }

    /// <summary>
    /// Player looks at interactable
    /// </summary>
    /// <param name="player"></param>
    public void BeginHighlight(Player player);

    /// <summary>
    /// Player stops looking at interactable
    /// </summary>
    /// <param name="player"></param>
    public void EndHighlight(Player player);

    /// <summary>
    /// Can the player start the interaction
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool CanInteract(Player player);

    /// <summary>
    /// Can the player manually exit the interaction
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool CanStopInteraction(Player player);

    /// <summary>
    /// Is the interaction over
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool InteractionOver(Player player);

    /// <summary>
    /// Player starts interacting
    /// </summary>
    /// <param name="player"></param>
    public void StartInteracting(Player player);

    /// <summary>
    /// Called every frame of the interaction
    /// </summary>
    /// <param name="player"></param>
    public void InteractionUpdate(Player player);

    /// <summary>
    /// Called every fixed timestep of the interaction
    /// </summary>
    /// <param name="player"></param>
    public void InteractionFixedUpdate(Player player);

    /// <summary>
    /// Called when the interaction ends
    /// </summary>
    /// <param name="player"></param>
    public void InteractionEnd(Player player);

}