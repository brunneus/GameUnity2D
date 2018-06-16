using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisateScript : MonoBehaviour {
	public GameObject passenger;
	public float speed;
	public float timeToWaitBus;
	private float startNenoPosition;
	private float lastNenoPosition;
	public string lastScene;

	private GameObject player;
	private bool firstStep, secondStep;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (firstStep) {
			if (startingLevel ()) {
				if (player.transform.position.x < startNenoPosition) {
					updatePosition (startNenoPosition + 10);
					updatePosition (startNenoPosition + 10, player.transform);
				} else {
					StartCoroutine (returnDriving ());
				}
			} else {
				if (player.transform.position.x > transform.position.x) {
					updatePosition (player.transform.position.x + 10);
				} else {
					StartCoroutine (returnDriving ());
				}
			}
		} else if (secondStep) {
			if (startingLevel ()) {
				updatePosition (startNenoPosition + 30);
				if (transform.position.x > (startNenoPosition + 20)) {
					secondStep = false;

					transform.position = new Vector3 (
						startNenoPosition - 20, 
						this.transform.position.y, 
						this.transform.position.z);

					player.GetComponent<PlayerPlatformerController> ().unlock ();
					player.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
				}
			} else {
				updatePosition (transform.position.x + 15);
				if (transform.position.x > (lastNenoPosition + 10)) {
					SceneManager.LoadScene (lastScene);
				}
			}
		}
	}

	public void CatchMe(GameObject localPlayer) {
		firstStep = true;

		transform.position = new Vector3(
			localPlayer.transform.position.x - 20, 
			this.transform.position.y, 
			this.transform.position.z);
	}

	public void LetMe(GameObject localPlayer) {
		player = localPlayer;
		localPlayer.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		startNenoPosition = localPlayer.gameObject.transform.position.x;
		firstStep = true;

		transform.position = new Vector3(
			startNenoPosition - 20, 
			this.transform.position.y, 
			this.transform.position.z);

		localPlayer.transform.position = new Vector3(
			startNenoPosition - 20, 
			localPlayer.transform.position.y, 
			localPlayer.transform.position.z);
	}

	private void updatePosition(float finalPosition) {
		updatePosition (finalPosition, transform);
	}

	private void updatePosition(float finalPosition, Transform objectTransform) {
		objectTransform.position = Vector3.Lerp(objectTransform.position, new Vector3(finalPosition, objectTransform.position.y, objectTransform.position.z), speed * Time.deltaTime);
	}
	 
	private IEnumerator returnDriving() {
		yield return new WaitForSeconds (timeToWaitBus);
		firstStep = false;
		secondStep = true;
		if (player != null && !startingLevel ()) {
			lastNenoPosition = player.transform.position.x;
			Destroy (player.gameObject);
			if (passenger != null) Destroy (passenger.gameObject);
		}
	}

	private bool startingLevel() {
		return player != null && player.transform.position.x < (passenger != null ? passenger.transform.position.x - 20 : 200);
	}
}
