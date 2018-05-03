using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public string levelToLoad;

	public void NewGame() {
        SceneManager.LoadScene(levelToLoad);
    }

    public void QuitGame() {
        Application.Quit(); 
    }

}
