using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxUpScript : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll)
	{
		var normal = coll.contacts [0].normal;




		if (normal.y < 0) {
			return;
		}

		var destination = new Vector3(transform.position.x, transform.position.y + (3f * normal.y), transform.position.z);

		transform.position = Vector3.Lerp(transform.position, destination, 0.1f);
		//StartCoroutine("Sample");
	}
}
