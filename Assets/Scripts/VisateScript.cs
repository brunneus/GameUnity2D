using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisateScript : MonoBehaviour {
	public GameObject passenger;
	public float speed;
	public float timeToWaitBus;

	private GameObject player;
	private bool firstStep, secondStep;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (firstStep) {
			if (player.transform.position.x > transform.position.x) {
				updatePosition (player.transform.position.x + 15);
			} else {
				StartCoroutine (returnDriving ());
			}
		} else if (secondStep) {
			updatePosition (transform.position.x + 20);
		}
	}

	public void CatchMe(GameObject localPlayer) {
		player = localPlayer;
		firstStep = true;

		transform.position = new Vector3(
			localPlayer.transform.position.x - 20, 
			this.transform.position.y, 
			this.transform.position.z);
	}

	private void updatePosition(float finalPosition) {
		//transform.Translate (Vector2.right * Time.deltaTime * 0.0001f);
		transform.position = Vector3.Lerp(transform.position, new Vector3(finalPosition, transform.position.y, transform.position.z), speed * Time.deltaTime);
	}
	 
	private IEnumerator returnDriving() {
		yield return new WaitForSeconds (timeToWaitBus);
		firstStep = false;
		secondStep = true;
		Destroy (player.gameObject);
		Destroy (passenger.gameObject);
	}
}
