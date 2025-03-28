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
    [BoxGroup("Components")][SerializeField] private Camera m_camera;
    [BoxGroup("Components")][SerializeField] private Tatzelcam m_playerCamera;
    [BoxGroup("Components")][SerializeField] private PlayerBrush m_brush;
    [BoxGroup("Components")][SerializeField] private PlayerInteractor m_interactor;
    [BoxGroup("Components")][SerializeField] private PlayerCameraFx m_cameraFx;
    [BoxGroup("Components")][SerializeField] private PlayerCutsceneController m_cutsceneController;
    [BoxGroup("Components")][SerializeField] private PlayerCameraController m_cameraController;
    [SerializeField] private Transform m_carryTransform;

    public PlayerState State => m_state;
    public Camera Camera => m_camera;
    public Tatzelcam PlayerCamera => m_playerCamera;
    public PlayerController Controller => m_controller;
    public PlayerBrush Brush => m_brush;
    public PlayerInteractor Interactor => m_interactor;
    public PlayerCameraFx CameraFx => m_cameraFx;
    public PlayerCutsceneController Cutscene => m_cutsceneController;
    public PlayerCameraController CameraController => m_cameraController;
    public Transform CarryTransform => m_carryTransform;
}