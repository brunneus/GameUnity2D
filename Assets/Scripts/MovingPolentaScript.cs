using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPolentaScript : MonoBehaviour {
	public int begin;
	public int end;
	public float speed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (transform.position.x <= begin || transform.position.x >= end) {
			speed *= -1;
		}
		transform.Translate (speed, 0, 0);
	}
}
