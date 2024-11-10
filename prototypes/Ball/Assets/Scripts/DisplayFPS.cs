using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayFPS : MonoBehaviour {

    private TextMeshProUGUI m_tx;
    private void Start() {
        m_tx = GetComponent<TextMeshProUGUI>();
        StartCoroutine(RefreshFPS());
        m_tx.text = $"...";
    }

    private IEnumerator RefreshFPS() {
        while (true) {
            yield return new WaitForSeconds(1);
            m_tx.text = $"ft: {Time.deltaTime * 1000:0.00}ms | {1 / Time.smoothDeltaTime:0.0}fps";
        }
    }
}
