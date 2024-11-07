using UnityEngine;

public class BulletBehavior : MonoBehaviour {
    public float lifeTime = 5f;

    void Start() {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
