using NaughtyAttributes;
using UnityEngine;

public class Tatzelcam : MonoBehaviour {
    public float fov;
    public Vector3 Position => transform.position;
    public Quaternion Rotation => transform.rotation;

    [Button("Test!")]
    private void Test() {
        Game.Player.Cutscene.AddCamera(gameObject.name, this);
    }

    [Button("Remove!")]
    private void Rm() {
        Game.Player.Cutscene.PopCamera(gameObject.name);
    }
}
