using UnityEngine;
using UnityEngine.Playables;

public class SkiMinigame : Interactable {
    [SerializeField] private Puzzle m_puzzle;
    [SerializeField] private PlayableDirector m_director;
    [SerializeField] private Tatzelcam m_camera;

    private bool m_hasLeft, m_hasRight;

    [SerializeField] private GameObject m_left, m_right;

    public override string Tooltip => "Escape";

    private void Start() {
        m_left.SetActive(m_hasLeft);
        m_right.SetActive(m_hasRight);
    }

    public void UpdatePuzzle() {
        m_hasLeft = m_puzzle.Count("ski") >= 1;
        m_hasRight = m_puzzle.Count("ski") >= 2;

        m_left.SetActive(m_hasLeft);
        m_right.SetActive(m_hasRight);
    }

    public override bool CanStopInteraction(Player player) => false;


    public override bool CanInteract(Player player) => m_hasLeft && m_hasRight;
    public override void InteractionEnd(Player player) {

    }

    public override void InteractionStart(Player player) {
        player.Cutscene.AddCamera("ending", m_camera);
        player.Controller.enabled = false;
        Game.Controller.SetSafe(true);

        Game.Controller.Victory();
        Game.UI.Win();
        m_director.Play();
    }
}
