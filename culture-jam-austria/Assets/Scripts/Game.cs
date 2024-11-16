using UnityEngine;

public class Game : MonoBehaviour {
    public static GameInput Input { get; private set; }

    private void Awake() {
        if (Input == null) {
            Input = new GameInput();
            Input.Enable();
        }

        DontDestroyOnLoad(gameObject);
    }


}