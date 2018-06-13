using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneScript : MonoBehaviour {

    public AudioClip audio;
	public string levelToLoad;
    
    void Start () {
        AudioSource.PlayClipAtPoint(audio, transform.position, 10);

		StartCoroutine(callNextScene());
    }

	private IEnumerator callNextScene() {
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene(levelToLoad);
	}
}
