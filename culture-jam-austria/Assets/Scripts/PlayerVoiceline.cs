using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Voice Line", menuName = "Game/VoiceLine")]
public class PlayerVoiceline : ScriptableObject {
    [System.Serializable]
    public enum VoiceLineType {
        CutsceneDialogue,
        Remark,
        SoundEffect
    }


    public VoiceLineType type;
    [Multiline] public string text;
    [HideIf("HasAudio")][SerializeField] private float m_duration;
    public AudioClip audio;

    public bool HasAudio => audio != null;
    public float Duration => audio == null ? m_duration : audio.length;
}
