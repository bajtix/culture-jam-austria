using UnityEngine;

public class PlayerCutsceneController : PlayerComponent {
    [SerializeField] private AudioSource m_voiceSource;


    private void Start() {
        Player.CameraController.AddCam("main", Player.PlayerCamera, 1);
    }

    public void PlayVoiceline(PlayerVoiceline line) {
        if (line.text.Contains("{"))
            Game.UI.Subtitles.MicroDvd(line.text, 0);
        else
            Game.UI.Subtitles.SingleSubtitle(line.text, 0, line.Duration);

        if (line.HasAudio)
            m_voiceSource.PlayOneShot(line.audio);
    }
}
