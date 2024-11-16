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
    [BoxGroup("Components")][SerializeField] private PlayerController m_controller;

    public PlayerState State => m_state;
    public PlayerController Controller => m_controller;
}