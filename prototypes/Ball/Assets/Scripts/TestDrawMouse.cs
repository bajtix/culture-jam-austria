using UnityEngine;

public class TestDrawMouse : MonoBehaviour {
    private void Update() {
        if (!Input.GetMouseButton(0)) return;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var info)) return;

        transform.position = info.point + transform.up;
    }
}
