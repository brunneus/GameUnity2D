using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {
	private static int score = 0;
	public static string lastSceneName;

	public static void resetScore() {
		score = 0;
	}

	public static int getScore() {
		return score;
	}

	public static void increaseScore() {
		score++;
	}
}
