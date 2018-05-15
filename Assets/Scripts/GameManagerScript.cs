using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {
	private static int score = 0;
	private static Text scoreText;
	public Text publicScoreText;

	// Use this for initialization
	void Start () {
		scoreText = publicScoreText;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void resetScore() {
		score = 0;
	}

	public static int getScore() {
		return score;
	}

	public static void increaseScore() {
		score++;
		scoreText.text = "Pontuação: " + score;
	}
}
