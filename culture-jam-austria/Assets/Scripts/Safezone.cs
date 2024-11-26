using UnityEngine;

public class Safezone : MonoBehaviour {
    [SerializeField] private bool m_isSecured = true;

    private void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Player")) Game.Controller.SetSafe(true && m_isSecured);
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.CompareTag("Player")) Game.Controller.SetSafe(false);
    }
}
