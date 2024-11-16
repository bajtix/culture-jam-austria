using UnityEngine;

public interface IInteractable {
    public string Tooltip { get; protected set; }

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
    /// Player starts interacting
    /// </summary>
    /// <param name="player"></param>
    /// <returns>Whether we agree to the interaction</returns>
    public bool StartInteracting(Player player);

}