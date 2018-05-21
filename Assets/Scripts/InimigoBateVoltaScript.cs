using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InimigoBateVoltaScript : MonoBehaviour {

	public float speed = 5;
	public int direction = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector2 (direction, 0) * speed * Time.deltaTime);
	}

	public void Dead() {
		speed = 0;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag != "Stone" && other.gameObject.tag != "Enemy" && other.gameObject.tag != "Point") {
			direction *= -1;
            if (direction == -1)
                GetComponent<SpriteRenderer>().flipX = true;
            else
                GetComponent<SpriteRenderer>().flipX =false;
        }
	}
}
