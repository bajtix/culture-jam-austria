using UnityEngine;

public class PlayerCameraFx : PlayerComponent {
    private struct BobType {
        public float horizontalStrength;
        public float horizontalFrequency;
        public float verticalStrength;
        public float verticalFrequency;
    }

    [SerializeField] private Camera m_camera;
    [SerializeField] private BobType m_walking;


    private float m_t;
    private float m_l;

    private Vector3 m_cameraPosition;

    private void Start() {
        m_cameraPosition = m_camera.transform.localPosition;
    }

    private void Update() {
        m_camera.transform.localPosition = m_cameraPosition;
    }


    public Camera Camera => m_camera;
}
