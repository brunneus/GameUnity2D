using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 5;
    public float jumpForce = 1200;

    [Range(0,1)]
    public float sliding = .9f;

	// Update is called once per frame
	void Update () {
		if (transform.position.y < -10)
			Destroy (gameObject);

        var h = Input.GetAxis("Horizontal");

        var grounded = IsGrounded();

        if(Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
        }

        var v = GetComponent<Rigidbody2D>().velocity;

        if(h != 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(h * speed, v.y);
            transform.localScale = new Vector2(Mathf.Sign(h), transform.localScale.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(v.x * sliding, v.y);
        }

        GetComponent<Animator>().SetFloat("speed", Mathf.Abs(h));
        GetComponent<Animator>().SetBool("jumping", !grounded);
    }

    private bool IsGrounded()
    {
        var bounds = GetComponent<Collider2D>().bounds;

        var range = bounds.size.y;

        var v = new Vector2(bounds.center.x, bounds.min.y - range);

        var hit = Physics2D.Linecast(v, bounds.center);

        return hit.collider.gameObject != gameObject;
    }
		
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForce);
			GetComponent<Collider2D> ().enabled = false;

			GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX;

			other.gameObject.GetComponent<InimigoBateVoltaScript> ().Dead ();

		} else if (other.gameObject.tag == "Point") {
			GameManagerScript.increaseScore ();
			Destroy (other.gameObject);
		}
	}
}
