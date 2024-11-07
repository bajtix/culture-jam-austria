using UnityEngine;

public class Shooting : MonoBehaviour {
    public float lifetime = 5f;
    public float bulletForce = 10f;
    public GameObject bullet;

    private void OnShoot() {
        var spawnedBullet = Instantiate(bullet, transform.position, Quaternion.LookRotation(Vector3.forward));
        spawnedBullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * bulletForce, ForceMode.Impulse);
        Destroy(spawnedBullet, lifetime);
    }

}
