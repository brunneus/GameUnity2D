using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {
	private static int score = 0;
	private static Text scoreText;
	public Text publicScoreText;
	public static string lastSceneName;

	// Use this for initialization
	void Start () {
		scoreText = publicScoreText;
		updateScoreText ();
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
		updateScoreText ();
	}

	private static void updateScoreText() {
		scoreText.text = "Bergas: " + score;
	}
}
