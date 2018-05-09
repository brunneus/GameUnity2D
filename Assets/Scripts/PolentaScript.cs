using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PolentaScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.y < -10) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		var coliders = other.contacts.Select (c => c.normal);

		if (coliders.Any (c => c == -Vector2.up)) {		
			StartCoroutine (waitDelay ());
		}
	}

	private IEnumerator waitDelay() {
		yield return new WaitForSeconds (.5f);
		this.gameObject.AddComponent<Rigidbody2D> ();
		GetComponent<Collider2D>().enabled = false;
	}
}
