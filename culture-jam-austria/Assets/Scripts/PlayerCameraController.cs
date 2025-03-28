using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCameraController : PlayerComponent {
    private Dictionary<string, (float weight, Tatzelcam cam)> m_cameras = new Dictionary<string, (float weight, Tatzelcam cam)>();

    private void LateUpdate() {
        float totalWeight = m_cameras.Sum(a => a.Value.weight);

        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        float fov = 0;

        if (totalWeight == 0 && m_cameras.Count > 0) {
            position = m_cameras.First().Value.cam.Position;
            rotation = m_cameras.First().Value.cam.Rotation;
            fov = m_cameras.First().Value.cam.fov;
        } else {
            foreach (var i in m_cameras) {
                position += i.Value.cam.Position * (i.Value.weight / totalWeight);
                rotation = Quaternion.Slerp(rotation, i.Value.cam.Rotation, i.Value.weight / totalWeight);
                fov = Mathf.Lerp(fov, i.Value.cam.fov, i.Value.weight / totalWeight);
            }
        }

        Player.Camera.transform.position = position;
        Player.Camera.transform.rotation = rotation;
        Player.Camera.fieldOfView = fov;
    }

    public void AddCam(string name, Tatzelcam cam, float value) {
        if (!IsCam(name)) {
            m_cameras.Add(name, (value, cam));
        } else {
            m_cameras[name] = (value, cam);
        }
    }

    public bool IsCam(string name) {
        return m_cameras.ContainsKey(name);
    }

    public void RemoveCam(string name) {
        if (IsCam(name)) {
            m_cameras.Remove(name);
        } else {
            Debug.LogWarning("removing nonexisting speed modifier " + name);
        }
    }
}
