using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDebugPane : MonoBehaviour {
    [SerializeField] private Slider m_progressBar;
    [SerializeField] private TextMeshProUGUI m_statusText;
    [SerializeField] private TextMeshProUGUI m_journal;

    private Dictionary<string, object> m_vars = new Dictionary<string, object>();
    private StringBuilder m_sb = new StringBuilder();

    public void JournalLog(string text) {
        m_journal.text = text + "<br>" + m_journal.text;
    }

    public void SetStatusVar(string t, object v) {
        if (m_vars.ContainsKey(t)) m_vars[t] = v;
        else m_vars.Add(t, v);
    }

    public void SetProgressBar(float f) {
        m_progressBar.value = f;
    }

    private void Update() {
        m_sb.Clear();

        foreach (var kv in m_vars) {
            m_sb.Append($"<line-height=0><b><align=left>{kv.Key}</b><br><align=right>{kv.Value}</align><line-height=1em><br>");
        }

        m_statusText.text = m_sb.ToString();
    }

}
