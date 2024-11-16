using UnityEngine;

public class Beartrapmechanic : MonoBehaviour
{
    [SerializeField]private GameObject m_bearTrap;
    [SerializeField] private PlayerController m_playerController;
    private float m_trapworktime = 5f;
    private bool m_isTrapActivated= false;
    private float m_timeSinceActivated = 0f;

    void Start(){
        
    }
    void Update(){
         if (m_isTrapActivated)
        {
            m_timeSinceActivated += Time.deltaTime;
            if (m_timeSinceActivated >= m_trapworktime)
            {
                m_playerController.RemoveSpeedModifier("Stop");
                m_isTrapActivated = false;
                m_timeSinceActivated = 0f; 
            }
        }
    }    
    private void OnTriggerEnter(Collider other){
            Trap_effect();   
            
    }

    private void Trap_effect() {
        m_isTrapActivated = true;
        m_timeSinceActivated = 0f; 
        m_playerController.AddSpeedModifier("Stop", 0f);
    }
}
