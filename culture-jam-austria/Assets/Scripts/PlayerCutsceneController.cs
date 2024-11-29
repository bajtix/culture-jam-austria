using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerCutsceneController : PlayerComponent {
    [SerializeField] private AudioSource m_voiceSource;

    [System.Serializable]

    private class CameraEntry {
        public string name;
        public Tatzelcam cam;
        public float w;
        public bool active;

        public CameraEntry(string name, Tatzelcam cam) {
            this.name = name;
            this.cam = cam;
            this.w = 0;
            this.active = true;
        }

        public override string ToString() => $"{name} = {w}";
    }

    [SerializeField] private List<CameraEntry> m_cameraStack = new List<CameraEntry>();

    public float transitionSpeed = 4;

    public void AddCamera(string n, Tatzelcam c) {
        if (m_cameraStack.Where(a => a.name == n).Count() != 0) {
            throw new System.Exception("Cannot add another camera '" + n + "', it already is on the stack");
        }
        m_cameraStack.Add(new CameraEntry(n, c));
    }

    public void PopCamera(string n) {
        var cams = m_cameraStack.Where(a => a.name == n);
        if (cams.Count() < 1) {
            Debug.LogWarning($"Cannot remove camera '{n}' as it is not on the stack.");
            return;
        }
        cams.First().active = false;
        cams.First().w = 0;
        var ce = new CameraEntry(Random.Range(0, 1000).ToString(), cams.First().cam);
        ce.w = 1;
        ce.active = false;
        m_cameraStack.Add(ce);
    }


    private void Update() {
        var lastAndActive = m_cameraStack.Where(w => w.active).Last();
        for (int i = 0; i < m_cameraStack.Count; i++) {
            if (!m_cameraStack[i].active && m_cameraStack[i].w < 0.001f) {
                Player.CameraController.RemoveCam(m_cameraStack[i].name);
                m_cameraStack.RemoveAt(i);
            } else {
                m_cameraStack[i].w = Mathf.Lerp(m_cameraStack[i].w, m_cameraStack[i] == lastAndActive ? 1 : 0, Time.deltaTime * transitionSpeed);
                if (m_cameraStack[i].w < 0.001f) m_cameraStack[i].w = 0;
                if (m_cameraStack[i].w > 1 - 0.001f) m_cameraStack[i].w = 1;
                Player.CameraController.AddCam(m_cameraStack[i].name, m_cameraStack[i].cam, m_cameraStack[i].w);
            }
        }
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
