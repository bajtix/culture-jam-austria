using UnityEngine;

public class PlayerCutsceneController : MonoBehaviour {
    [SerializeField] private AudioSource m_voiceSource;

    public void PlayVoiceline(PlayerVoiceline line) {
        if (line.text.Contains("{"))
            Game.UI.Subtitles.MicroDvd(line.text, 0);
        else
            Game.UI.Subtitles.SingleSubtitle(line.text, 0, line.Duration);

        m_voiceSource.PlayOneShot(line.audio);
    }
}
