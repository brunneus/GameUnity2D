﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneScript : MonoBehaviour {

    public AudioClip audio;
	public string levelToLoad;
    
    void Start () {
		GameManagerScript.lastSceneName = levelToLoad;
        AudioSource.PlayClipAtPoint(audio, transform.position, 10);

		if (levelToLoad != null) StartCoroutine(callNextScene());
    }

	private IEnumerator callNextScene() {
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene(levelToLoad);
	}
}
