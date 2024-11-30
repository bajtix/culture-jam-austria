using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour {
    [System.Serializable]
    private class Piece {
        public string puzzleTag;
        public bool has;
        public GameObject highlight;
    }

    [SerializeField] private List<Piece> m_pieces = new List<Piece>();
    [SerializeField] private UnityEvent m_puzzleUpdated;

    public bool CanDeliever(string s) {
        return m_pieces.Where(w => w.puzzleTag == s && !w.has).Count() >= 1;
    }

    public void Deliever(string s) {
        var fit = m_pieces.Where(w => w.puzzleTag == s && !w.has);
        if (fit.Count() > 0) {
            fit.First().has = true;
            try {
                m_puzzleUpdated.Invoke();
            } catch (Exception e) {
                Debug.LogError("Exception when invoking puzzle update!");
                Debug.LogError(e);
            }
        }
    }

    public bool IsComplete() {
        return m_pieces.Select(a => a.has).Aggregate((a, b) => a && b);
    }

    public void ConsumeAll() {
        foreach (var d in m_pieces) {
            d.has = false;
        }
        try {
            m_puzzleUpdated.Invoke();
        } catch (Exception e) {
            Debug.LogError("Exception when invoking puzzle update!");
            Debug.LogError(e);
        }
    }


    public bool Has(string s) {
        return Count(s) > 0;
    }

    public int Count(string s) {
        return m_pieces.Count(w => w.puzzleTag == s && w.has);
    }

    public void OnGetHoldOf(string s) {
        foreach (var d in m_pieces) {
            if (d.highlight != null)
                d.highlight.SetActive(false);
        }
        foreach (var d in m_pieces.Where(d => !d.has && d.puzzleTag == s)) {
            if (d.highlight != null)
                d.highlight.SetActive(true);
        }
    }

}
