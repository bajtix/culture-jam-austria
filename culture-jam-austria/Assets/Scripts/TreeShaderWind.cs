using NaughtyAttributes;
using UnityEngine;

[ExecuteAlways]
public class TreeShaderWind : MonoBehaviour {

    [SerializeField] private Vector3 m_bend;
    [SerializeField] private Vector3 m_branchBend;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_branchSpeed;

    [SerializeField] private float m_changeSpeed = 0.5f;
    [SerializeField] private float m_bendMultiplier = 1, m_speedMultiplier = 1;
    [SerializeField][ReadOnly] private float m_actualBendMultiplier = 1, m_actualSpeedMultiplier = 1;



    public void SetMultipliers(float bendMultiplier, float speedMultiplier) {
        m_bendMultiplier = bendMultiplier;
        m_speedMultiplier = speedMultiplier;
    }


    private void Update() {
        m_actualBendMultiplier = Mathf.Lerp(m_actualBendMultiplier, m_bendMultiplier, Time.deltaTime * m_changeSpeed);
        if (Mathf.Abs(m_actualBendMultiplier - m_bendMultiplier) > 0.001) {
            print("update bend");
            Shader.SetGlobalVector("_Bend", m_bend * m_actualBendMultiplier);
            Shader.SetGlobalVector("_BranchBend", m_branchBend * m_actualBendMultiplier);
        }

        m_actualSpeedMultiplier = Mathf.Lerp(m_actualSpeedMultiplier, m_speedMultiplier, Time.deltaTime * m_changeSpeed);
        if (Mathf.Abs(m_actualSpeedMultiplier - m_speedMultiplier) > 0.001) {
            Shader.SetGlobalFloat("_Speed", m_speed * m_actualSpeedMultiplier);
            Shader.SetGlobalFloat("_BranchSpeed", m_branchSpeed * m_actualSpeedMultiplier);
        }
    }
}
