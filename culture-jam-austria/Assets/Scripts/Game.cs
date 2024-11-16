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

    private void Awake() {
        Game.Input.Enable();

        DontDestroyOnLoad(gameObject);
    }


}