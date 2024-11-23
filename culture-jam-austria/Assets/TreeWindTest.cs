using UnityEngine;

[ExecuteAlways]
public class TreeWindTest : MonoBehaviour {

    [SerializeField] private Vector3 m_bend;
    [SerializeField] private Vector3 m_branchBend;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_branchSpeed;



    private void Update() {
        Shader.SetGlobalVector("_Bend", m_bend);
        Shader.SetGlobalVector("_BranchBend", m_branchBend);
        Shader.SetGlobalFloat("_Speed", m_speed);
        Shader.SetGlobalFloat("_BranchSpeed", m_branchSpeed);
    }
}
