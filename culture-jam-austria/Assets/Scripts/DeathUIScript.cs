using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class DeathUIScript : MonoBehaviour
{
	[SerializeField] private Image fadeInOut;
	[SerializeField] private GameObject deathPanel;
	[SerializeField] private float fadeSpeed;
	float alpha;
	bool isDying = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		deathPanel.SetActive(false);
		alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
		if (isDying) {
			Debug.Log("aaaa");
			alpha += fadeSpeed * Time.deltaTime;
		} else {
			alpha -= fadeSpeed * Time.deltaTime;
		}


		if (alpha >= 1) {
			alpha = 1;
			if (isDying) {
				fadeInOut.gameObject.SetActive(false);
				deathPanel.SetActive(true);				
				Cursor.lockState = CursorLockMode.None;
			}
		}
		if (alpha < 0) {
			fadeInOut.gameObject.SetActive(false);
			alpha = 0;
		}
		if(alpha>0 && alpha<1){
			fadeInOut.gameObject.SetActive(true);
		}
		fadeInOut.color = new Color(0, 0, 0, alpha);
    }
	public void Die() {
		Debug.Log("die");
		isDying = true;
	}

	public void Restart() {
		Debug.Log("restart");
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
