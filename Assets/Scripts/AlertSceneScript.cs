using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlertSceneScript : MonoBehaviour {
	public Button backButton;

	void Awake() {
		backButton.onClick.AddListener (BackToMenu);
	}

	public void BackToMenu() {
		SceneManager.LoadScene ("MainMenu");
	}
}
