using System.Collections.Generic;
using LitMotion;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroShitscene : MonoBehaviour {
    [SerializeField] private float m_fadeSpeed = 0.4f;
    [SerializeField] private CanvasGroup[] m_panels;
    [SerializeField] private CanvasGroup m_fader, m_transfader;

    private List<MotionHandle> m_anims = new List<MotionHandle>();
    [SerializeField] private int m_panel;



    private void Start() {
        LMotion.Create(1f, 0f, m_fadeSpeed)
        .Bind((x) => m_fader.alpha = x);
    }

    public void NextPanel() {
        float delay = 0;

        foreach (var c in m_anims) if (c.IsActive()) c.Complete();
        m_anims.Clear();

        if (m_panel >= m_panels.Length) return;

        if (m_panel == m_panels.Length - 1) {
            LMotion.Create(0f, 1f, m_fadeSpeed)
            .Bind((x) => m_transfader.alpha = x);

            delay += m_fadeSpeed + 2f;

            LMotion.Create(0f, 1f, m_fadeSpeed)
            .WithDelay(delay)
            .WithOnComplete(() => SceneManager.LoadScene(1))
            .Bind((x) => m_fader.alpha = x);

            m_panel++;

            return;
        }

        m_panel++;
        m_anims.Add(
            LMotion.Create(1f, 0f, m_fadeSpeed)
            .WithDelay(delay)
            .WithOnComplete(() =>
                m_panels[m_panel].gameObject.SetActive(true)
            )
            .Bind((x) => m_panels[m_panel - 1].alpha = x)
        );

        delay += m_fadeSpeed;

        m_anims.Add(
            LMotion.Create(0f, 1f, m_fadeSpeed)
            .WithDelay(delay)
            .Bind((x) => m_panels[m_panel].alpha = x)
        );
    }
}
