using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject player;
	private Vector3 m_offset;

	private void Start() {
		m_offset = transform.position - player.transform.position;
	}

	private void LateUpdate() {
		transform.position = player.transform.position + m_offset;
	}
}
