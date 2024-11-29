using UnityEngine;

public class PlayerCameraFx : PlayerComponent {
    [System.Serializable]
    private struct BobType {
        public float horizontalStrength;
        public float horizontalFrequency;
        public float verticalStrength;
        public float verticalFrequency;
    }

    [SerializeField] private float m_smoothing = 2;
    [SerializeField] private BobType m_walking, m_sprinting, m_panting;


    private Vector3 m_cameraPosition;
    private float m_bobbingTime;
    private float m_bobbingStrength;
    private Vector3 m_shakeVector;
    private float m_shakeStrength;
    private float m_shakeTimeLeft;

    private void Start() {
        m_cameraPosition = Player.PlayerCamera.transform.localPosition;
    }

    private Vector3 EvaluateBobbing(BobType type, float time) {
        return Mathf.Sin(time * type.verticalFrequency) * type.verticalStrength * Vector3.up
                + Mathf.Cos(time * type.horizontalFrequency) * type.horizontalStrength * Vector3.right;
    }

    private void Update() {
        float walkspeed = Mathf.Clamp01(Player.Controller.Velocity / (Player.Controller.MaxSpeed + 0.001f));
        m_bobbingStrength = Mathf.Lerp(m_bobbingStrength, walkspeed, Time.deltaTime * m_smoothing);

        Vector3 vtoa;

        if (Player.Controller.IsSprinting) {
            vtoa = EvaluateBobbing(m_sprinting, m_bobbingTime) * m_bobbingStrength;
        } else if (Player.Controller.IsTired) {
            vtoa = EvaluateBobbing(m_panting, Time.time) + EvaluateBobbing(m_walking, m_bobbingTime) * m_bobbingStrength;
        } else {
            vtoa = EvaluateBobbing(m_walking, m_bobbingTime) * m_bobbingStrength;
        }
        m_bobbingTime += walkspeed * Time.deltaTime;


        Player.PlayerCamera.transform.localPosition = m_cameraPosition + vtoa + m_shakeVector;
    }

    private void FixedUpdate() {
        m_shakeVector = Random.onUnitSphere * m_shakeStrength * Mathf.Clamp01(m_shakeTimeLeft);
        if (m_shakeTimeLeft > 0) {
            m_shakeTimeLeft -= Time.fixedDeltaTime;
        } else {
            m_shakeTimeLeft = 0;
        }
    }

    public void Shake(float strength, float duration) {
        m_shakeTimeLeft = duration;
        m_shakeStrength = strength;
    }


}
