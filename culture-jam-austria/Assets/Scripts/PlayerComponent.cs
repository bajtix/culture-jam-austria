using UnityEngine;
using NaughtyAttributes;

public abstract class PlayerComponent : MonoBehaviour {
    [BoxGroup("Components")][SerializeField] private Player m_player;
    public Player Player {
        get {
            if (m_player == null) {
                m_player = GetComponent<Player>();
            }
            return m_player;
        }
    }
}