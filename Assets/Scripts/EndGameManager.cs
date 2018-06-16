using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour {

    public Text scoreText;

    public void TryAgain()
    {
        SceneManager.LoadScene("MainMenu");
		GameManagerScript.resetScore ();
    }

    // Use this for initialization
    void Start () {
        if(GameManagerScript.getScore() <= 1) 
		    scoreText.text = "Você coletou " + GameManagerScript.getScore() + " berga! :(";
        else
		    scoreText.text = "Você coletou " + GameManagerScript.getScore() + " bergas!";
	}

	public void RepeatLevel() {
		SceneManager.LoadScene (GameManagerScript.lastSceneName);
	}
}
