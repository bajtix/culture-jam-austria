using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class UISubtitles : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI m_textBox;

    private List<(float at, string content)> m_queue = new List<(float at, string content)>() {
        (0,"")
    };

    public void SingleSubtitle(string text, float when, float duration) {
        float start = Time.time + when;
        float end = start + duration;

        bool interrupted = false;
        for (int i = 0; i < m_queue.Count; i++) {
            if (m_queue[i].at >= start && m_queue[i].at < end) {
                if (m_queue[i].content == "") m_queue.RemoveAt(i);
                else interrupted = true;
            }
        }

        m_queue.Add((start, text));

        if (!interrupted)
            m_queue.Add((end, ""));

        m_queue.Sort((a, b) => (int)Mathf.Sign(a.at - b.at));
    }

    public void MicroDvd(string text, float when = 0) {
        string[] lines = text.Split('\n');

        foreach (string line in lines) {
            if (string.IsNullOrWhiteSpace(line)) continue;
            try {

                string[] parts = line.Trim().Split(new[] { '}', '{' }, System.StringSplitOptions.RemoveEmptyEntries);
                float start = int.Parse(parts[0]) / 1000f;
                float end = int.Parse(parts[1]) / 1000f;
                string content = parts[2].Trim();
                SingleSubtitle(content, when + start, end - start);
            } catch {
                Debug.LogWarning("Failed parsing subtitle line\n" + line);
            }
        }
    }


    private void FixedUpdate() {
        for (int i = 0; i < m_queue.Count; i++) {
            if (m_queue[i].at <= Time.time) {
                m_textBox.SetText(m_queue[i].content);
                m_queue.RemoveAt(i);
            }
        }
    }
}
