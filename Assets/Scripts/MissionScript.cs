using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionScript : MonoBehaviour {
	public string levelToLoad;
	public float secondsToWait = 3;

	// Use this for initialization
	void Start () {
		StartCoroutine (waitDelay ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator waitDelay() {
		yield return new WaitForSeconds (secondsToWait);
		SceneManager.LoadScene (levelToLoad);
	}
}
