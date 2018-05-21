using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {


    public AnimationCurve curve; 
	private Vector2 normal;

    void OnCollisionEnter2D(Collision2D coll)
    {
		var normal = coll.contacts [0].normal;

		if (normal.y !=0) {
			return;
		}

		var destination = new Vector3(transform.position.x + (3f * normal.x), transform.position.y, transform.position.z);

		transform.position = Vector3.Lerp(transform.position, destination, 0.1f);
		//StartCoroutine("Sample");
	}

    IEnumerator Sample()
    {
        var pos = transform.position;

        for (float t = 0; t < curve.keys[curve.length - 1].time; t += Time.deltaTime)
        {
			transform.position = new Vector2(pos.x + (curve.Evaluate(t) * normal.x), pos.y);
            yield return null;
        }
    }
}
