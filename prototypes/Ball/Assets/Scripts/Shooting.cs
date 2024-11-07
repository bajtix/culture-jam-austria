using UnityEngine;

public class Shooting : MonoBehaviour {
    public GameObject Bullet;
    public Transform ShellCase;
    private Quaternion initialRotation;

    void Start() {
        initialRotation = ShellCase.rotation;
    }

    void Update() {
        ShellCase.rotation = initialRotation;

        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Shooting!");
            Instantiate(Bullet, ShellCase.position, ShellCase.rotation);
        }
    }
}
