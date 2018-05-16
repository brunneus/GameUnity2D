﻿using System.Collections;
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
		scoreText.text = GameManagerScript.getScore().ToString();
	}

    // Update is called once per frame
    void Update () {
		
	}
}
