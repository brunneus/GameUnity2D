using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PolentaScript : MonoBehaviour {
	// Use this for initialization
	public float fallOfSpeed = .5f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.y < -10) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		var normal = coll.contacts [0].normal;

		if (normal == -Vector2.up) {		
			StartCoroutine (waitDelay ());
		}
	}

	private IEnumerator waitDelay() {
		yield return new WaitForSeconds (this.fallOfSpeed);
		this.gameObject.AddComponent<Rigidbody2D> ();
		GetComponent<Collider2D>().enabled = false;
	}
}
