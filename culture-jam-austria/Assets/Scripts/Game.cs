using UnityEngine;

public class Game : MonoBehaviour {
    // Statics

    private static GameInput m_input;
    private static Game m_instance;

    public static GameInput Input {
        get {
            if (m_input == null) {
                m_input = new GameInput();
            }
            return m_input;
        }
    }

    public static Game Instance {
        get {
            if (m_instance == null) {
                m_instance = FindFirstObjectByType<Game>();
            }
            return m_instance;
        }
    }

    // Assignables

    [SerializeField] private UIManager m_uIManager;
    [SerializeField] private Player m_player;
    [SerializeField] private Blizzard m_blizzard;
    [SerializeField] private WindSound m_windSound;
    [SerializeField] private GameController m_gameController;
    [SerializeField] public LayerMask interactionMask = int.MaxValue;

    public static UIManager UI {
        get {
            if (Instance.m_uIManager == null) Instance.m_uIManager = FindFirstObjectByType<UIManager>();
            return Instance.m_uIManager;
        }
    }

    public static Player Player {
        get {
            if (Instance.m_player == null) Instance.m_player = FindFirstObjectByType<Player>();
            return Instance.m_player;
        }
    }

    public static Blizzard Blizzard {
        get {
            if (Instance.m_blizzard == null) Instance.m_blizzard = FindFirstObjectByType<Blizzard>();
            return Instance.m_blizzard;
        }
    }

    public static WindSound WindSound {
        get {
            if (Instance.m_windSound == null) Instance.m_windSound = FindFirstObjectByType<WindSound>();
            return Instance.m_windSound;
        }
    }

    public static GameController Controller {
        get {
            if (Instance.m_gameController == null) Instance.m_gameController = FindFirstObjectByType<GameController>();
            return Instance.m_gameController;
        }
    }

    private void Awake() {
        Game.Input.Enable();
        DontDestroyOnLoad(gameObject);
    }


}