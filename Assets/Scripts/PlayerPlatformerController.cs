using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlatformerController : PhysicsObject {

	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 7;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	public string levelToLoad;

	// Use this for initialization
	void Awake () 
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();   
		animator = GetComponent<Animator> ();
	}

	protected override void ComputeVelocity()
	{
		if (transform.position.y < -10) {
			Destroy (gameObject);

			SceneManager.LoadScene(levelToLoad);
		}

		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis ("Horizontal");

		if (Input.GetButtonDown ("Vertical") && grounded) {
			velocity.y = jumpTakeOffSpeed;
		} 
		else if (Input.GetButtonUp ("Jump")) 
		{
			if (velocity.y > 0) {
				velocity.y = velocity.y * 0.5f;
			}
		}

		if (move.x != 0) {
			transform.localScale = new Vector2 (Mathf.Sign (move.x), transform.localScale.y);
		}

		animator.SetBool ("grounded", grounded);
		animator.SetBool ("PlayerMoving", (Mathf.Abs (velocity.x) / maxSpeed) > 0);

		targetVelocity = move * maxSpeed;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpTakeOffSpeed);
			GetComponent<Collider2D> ().enabled = false;

			Debug.Log (jumpTakeOffSpeed);

			GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX;

			other.gameObject.GetComponent<InimigoBateVoltaScript> ().Dead ();

		} else if (other.gameObject.tag == "Point") {
			GameManagerScript.increaseScore ();
			Destroy (other.gameObject);
		}
	}
}