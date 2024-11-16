using UnityEngine;

public class Game : MonoBehaviour {
    private static GameInput m_input;
    public static GameInput Input {
        get {
            if (m_input == null) {
                m_input = new GameInput();
            }
            return m_input;
        }
    }

    private static Game m_instance;
    public static Game Instance {
        get {
            if (m_instance == null) {
                m_instance = FindFirstObjectByType<Game>();
            }
            return m_instance;
        }
    }

    [SerializeField] private UIManager m_uIManager;
    public static UIManager UI => Instance.m_uIManager;

    private void Awake() {
        Game.Input.Enable();
        DontDestroyOnLoad(gameObject);
    }


}