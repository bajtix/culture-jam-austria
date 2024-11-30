using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class IntroShitscene : MonoBehaviour {


    public void SceneLoad(int numberScene){
        SceneManager.LoadScene(numberScene);
    }
}
