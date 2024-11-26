using UnityEngine;

[CreateAssetMenu(fileName = "Voice Line", menuName = "Game/VoiceLine")]
public class PlayerVoiceline : ScriptableObject
{
    [System.Serializable]
    public enum VoiceLineType
    {
        CutsceneDialogue,
        Remark,
        SoundEffect
    }


    public VoiceLineType type;
    [Multiline] public string text;
    public AudioClip audio;

    public float Duration => audio.length;
}
