using UnityEngine;

public class DiggingSystem : MonoBehaviour{
    [SerializeField] private GameObject m_diggingArea;
    [SerializeField] private GameObject m_clickF;

    void OnCollisionEnter(Collision other)
    {
        ShowInfo();
    }

    void ShowInfo(){
        m_clickF.SetActive(true);
    }
}
