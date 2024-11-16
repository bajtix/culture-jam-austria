using UnityEngine;

public class POIScript : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			Debug.Log("Player entered " + gameObject.name + " POI");
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			Debug.Log("Player exited " + gameObject.name + " POI");
		}
	}
}
