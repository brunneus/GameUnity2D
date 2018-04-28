using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolentaScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other) {
		StartCoroutine (waitDelay ());
	}

	private IEnumerator waitDelay() {
		yield return new WaitForSeconds (.5f);

		Destroy (gameObject);
		GetComponent<Collider2D>().enabled = false;
	}
}
