using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPolentaScript : MonoBehaviour {
	public int begin;
	public int end;
	public float speed = 0.1f;
	public bool flip = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (transform.localPosition.x <= begin || transform.localPosition.x >= end) {
			speed *= -1;

			if (flip) {			
				if (speed < 0)
					GetComponent<SpriteRenderer>().flipX = true;
				else
					GetComponent<SpriteRenderer>().flipX =false;
			}

		}


		transform.Translate (speed, 0, 0);
	}
}
