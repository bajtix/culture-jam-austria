using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
	public void LoadScene(int x) {
		Time.timeScale = 1f;
		SceneManager.LoadScene(x);
	}
	public void Exit() {
		Application.Quit();
		Debug.Log("Exit");
	}
}
