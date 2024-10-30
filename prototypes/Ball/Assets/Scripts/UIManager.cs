using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
	public void LoadScene(int x) {
		SceneManager.LoadScene(x);
	}
	public void Extit() {
		Application.Quit();
		Debug.Log("Exit");
	}
}
