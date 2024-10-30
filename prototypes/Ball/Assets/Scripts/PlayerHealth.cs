using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public GameObject healthBar;
	public float health;

	private void Start() {
		healthBar.GetComponent<Slider>().value = health;
	}

	public void AddHealth(float howMuch) {
		health += howMuch;
		health = Mathf.Clamp(health, 0, 1);
		healthBar.GetComponent<Slider>().value = health;
	}

	public void SubtractHealth(float howMuch) {
		health -= howMuch;
		health = Mathf.Clamp(health, 0, 1);
		healthBar.GetComponent<Slider>().value = health;
	}
}
