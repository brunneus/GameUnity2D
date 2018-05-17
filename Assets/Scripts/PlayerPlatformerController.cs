using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class PlayerPlatformerController : PhysicsObject {

	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 7;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	public RawImage lifeDisplay;
	public string levelToLoad;
	public AudioClip[] coinSound;

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
		Vector2 sizeDisplayLife = this.lifeDisplay.GetComponent<RectTransform> ().sizeDelta;

		if (other.gameObject.tag == "Enemy") {
			Vector2 newSize = new Vector2 (sizeDisplayLife.x - 24, sizeDisplayLife.y);
			this.lifeDisplay.GetComponent<RectTransform> ().sizeDelta = newSize;

			StartCoroutine (this.executeEnemyHitEffectOnLifeDisplay ());


			if (newSize.x <= 0) {
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpTakeOffSpeed)	;
				GetComponent<Collider2D> ().enabled = false;
				GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX;

				other.gameObject.GetComponent<InimigoBateVoltaScript> ().Dead ();
			}

		} else if (other.gameObject.tag == "Point") {

            GameManagerScript.increaseScore();
			PlaySound (coinSound [0]);

			if (sizeDisplayLife.x < 160) {
				Vector2 newSize = new Vector2 (sizeDisplayLife.x + 24, sizeDisplayLife.y);
				this.lifeDisplay.GetComponent<RectTransform> ().sizeDelta = newSize;
				StartCoroutine (this.executeLifeHitEffectOnLifeDisplay ());
			}

			Destroy (other.gameObject);
		}
	}

	void PlaySound(AudioClip audio) {
		AudioSource.PlayClipAtPoint (audio, transform.position, 5);

	}

	private IEnumerator executeEnemyHitEffectOnLifeDisplay() {
		this.lifeDisplay.color = Color.white;
		yield return new WaitForSeconds (.05f);
		this.lifeDisplay.color = Color.red;
	}

	private IEnumerator executeLifeHitEffectOnLifeDisplay() {
		this.lifeDisplay.color = Color.green;
		yield return new WaitForSeconds (.05f);
		this.lifeDisplay.color = Color.red;
	}
}