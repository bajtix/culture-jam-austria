using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Audio;


[CreateAssetMenu(menuName = "Game/Sound Bite", fileName = "Sound")]
public class SoundBite : ScriptableObject {
    public AudioClip clip;
    public AudioMixerGroup group;
    [MinMaxSlider(0.5f, 2f)] public Vector2 pitch = new Vector2(0.9f, 1.1f);
    [MinMaxSlider(0.5f, 2f)] public Vector2 volume = new Vector2(0.9f, 1.1f);
    public float GetPitch() => Random.Range(pitch.x, pitch.y);
    public float GetVolume() => Random.Range(volume.x, volume.y);

}
