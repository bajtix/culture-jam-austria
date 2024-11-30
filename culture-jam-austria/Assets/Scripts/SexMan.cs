using UnityEngine;


/// <summary>
/// sound effects manager
/// </summary>
public class SexMan : MonoBehaviour {
    public void Play(SoundBite bite, Vector3 location, float threed) {
        var src = new GameObject().AddComponent<AudioSource>();
        src.spatialBlend = threed;
        src.volume = bite.GetVolume();
        src.pitch = bite.GetPitch();
        var clip = bite.GetClip();
        src.clip = clip;
        src.outputAudioMixerGroup = bite.group;
        src.playOnAwake = false;

        src.transform.position = location;

        src.Play();
        Destroy(src, clip.length);
    }
}
