using System;
using UnityEngine;

public class Brush : MonoBehaviour {
    public DynamicDraw surface;

    public float motionThreshhold = 0.1f;

    private Vector3 m_lastPosition;

    private void FixedUpdate() {
        if ((m_lastPosition - transform.position).sqrMagnitude < motionThreshhold * motionThreshhold) {
            return;
        } else {
            m_lastPosition = transform.position;
        }

        if (!Physics.Raycast(transform.position, Vector3.down, out var hit, 1f))
            return;

        surface.AddSplat(hit.textureCoord2);
    }
}
