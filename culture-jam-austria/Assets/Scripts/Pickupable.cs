using NaughtyAttributes;
using UnityEngine;

public class Pickupable : Interactable {
    public override string Tooltip => "Pick up";
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private Collider m_collider;
    [SerializeField] private string m_puzzleTag;
    [SerializeField] private float m_maxPlaceDistance = 2;
    [BoxGroup("Carrying settings")][SerializeField] protected float m_carrySpeed = 0.9f;
    [BoxGroup("Carrying settings")][SerializeField] private Vector3 m_carryPosition;
    [BoxGroup("Carrying settings")][SerializeField] private Vector3 m_carryRotation;


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

        transform.position = player.transform.position;
        transform.SetParent(null);
    }

    public override void InteractionStart(Player player) {
        player.Controller.AddSpeedModifier("carry", m_carrySpeed);
        m_rigidbody.isKinematic = true;
        m_collider.enabled = false;

        var puzzles = FindObjectsByType<Puzzle>(FindObjectsSortMode.None);
        foreach (var w in puzzles) {
            w.OnGetHoldOf(m_puzzleTag);
        }

        transform.SetParent(player.CarryTransform);

    }
    public override void InteractionUpdate(Player player) {

        transform.localPosition = m_carryPosition;
        transform.localRotation = Quaternion.Euler(m_carryRotation);
        Game.UI.SetInteractionTooltip("Drop");
        if (Physics.Raycast(player.PlayerCamera.transform.position, player.PlayerCamera.transform.forward, out var hit, m_maxPlaceDistance, Game.Instance.interactionMask)) {
            var puzzle = hit.collider.GetComponent<Puzzle>();
            if (puzzle != null && puzzle.CanDeliever(m_puzzleTag)) {
                Game.UI.SetInteractionTooltip("Place");
            }
        }
    }

}
