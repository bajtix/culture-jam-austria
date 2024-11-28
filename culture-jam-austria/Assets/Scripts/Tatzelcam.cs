using UnityEngine;

public class Tatzelcam : MonoBehaviour {
    public float fov;
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;
}
