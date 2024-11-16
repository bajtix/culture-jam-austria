using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public enum PlayerState {
    Menu,
    Walking,
    Minigame
}

public class Player : MonoBehaviour {
    [SerializeField][ReadOnly] private PlayerState m_state;
}