using UnityEngine;

public class Pickupable : Interactable {
    public override string Tooltip => "Pick up";
    [SerializeField] protected float m_weight = 1;
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private Collider m_collider;
    [SerializeField] private string m_puzzleTag;
    [SerializeField] private float m_maxPlaceDistance = 3;


    public override void InteractionEnd(Player player) {
        var puzzles = FindObjectsByType<Puzzle>(FindObjectsSortMode.None);
        foreach (var w in puzzles) {
            w.OnGetHoldOf("");
        }

        Game.UI.HideInteractionTooltip();
        if (Physics.Raycast(player.PlayerCamera.transform.position, player.PlayerCamera.transform.forward, out var hit, m_maxPlaceDistance, Game.Instance.interactionMask)) {
            var puzzle = hit.collider.GetComponent<Puzzle>();
            if (puzzle != null && puzzle.CanDeliever(m_puzzleTag)) {
                puzzle.Deliever(m_puzzleTag);
                Destroy(gameObject);
            }
        }

        player.Controller.RemoveSpeedModifier("carry");
        m_rigidbody.isKinematic = false;
        m_collider.enabled = true;
    }

    public override void InteractionStart(Player player) {
        player.Controller.AddSpeedModifier("carry", Mathf.Clamp(25f / (m_weight * m_weight), 0.1f, 1f));
        m_rigidbody.isKinematic = true;
        m_collider.enabled = false;

        var puzzles = FindObjectsByType<Puzzle>(FindObjectsSortMode.None);
        foreach (var w in puzzles) {
            w.OnGetHoldOf(m_puzzleTag);
        }
    }
    public override void InteractionUpdate(Player player) {
        transform.position = player.PlayerCamera.transform.position + player.PlayerCamera.transform.forward;

        Game.UI.SetInteractionTooltip("Drop");
        if (Physics.Raycast(player.PlayerCamera.transform.position, player.PlayerCamera.transform.forward, out var hit, m_maxPlaceDistance, Game.Instance.interactionMask)) {
            var puzzle = hit.collider.GetComponent<Puzzle>();
            if (puzzle != null && puzzle.CanDeliever(m_puzzleTag)) {
                Game.UI.SetInteractionTooltip("Place");
            }
        }
    }

}
