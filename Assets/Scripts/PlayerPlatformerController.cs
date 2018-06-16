using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class PlayerPlatformerController : PhysicsObject {

	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 7;
	public GameObject visate;
	public int sceneStartingIn;

	private SpriteRenderer spriteRenderer;
	private Animator animator;
	private bool locked;
	private bool playingSound;
	private bool canTakeDamage = true;
	private bool canMakeDamageEffects = true;

	public Color goodWealthyColor;
	public Color mediumWealthyColor;
	public Color badWealthyColor;

	public RawImage lifeDisplay;
	public string levelToLoad;
	public AudioClip[] coinSound;
	public AudioClip[] jumpSound;
	public AudioClip[] touchEnemySound;
    public AudioClip[] merdaSound;

    // Use this for initialization
    void Awake () 
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();   
		animator = GetComponent<Animator> ();
		locked = true;
		visate.GetComponent<VisateScript> ().LetMe (this.gameObject);
	}

	protected override void ComputeVelocity()
	{		
		if (this.canMakeDamageEffects && !canTakeDamage) {
			StartCoroutine (this.makeDamageEffect ());
		}

		if (!locked) {
			if (transform.position.y < -10) {
				Destroy (gameObject);

				SceneManager.LoadScene (levelToLoad);
			}

			Vector2 move = Vector2.zero;

			if (transform.position.x > sceneStartingIn || !(Input.GetAxis ("Horizontal") < 0)) {
				move.x = Input.GetAxis ("Horizontal");

				if (Input.GetKeyDown (KeyCode.UpArrow) && grounded) {
					velocity.y = jumpTakeOffSpeed;
					PlaySound (jumpSound [0]);
				} else if (Input.GetButtonUp ("Jump")) {
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
		} else {
			animator.SetBool ("grounded", true);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		Vector2 sizeDisplayLife = this.lifeDisplay.GetComponent<RectTransform> ().sizeDelta;

		if (other.gameObject.tag == "Enemy" && canTakeDamage) {
			PlayMerdaSound (merdaSound [Random.Range (0, 14)]);
            
			Vector2 newSize = new Vector2 (sizeDisplayLife.x - 24, sizeDisplayLife.y);
			this.lifeDisplay.GetComponent<RectTransform> ().sizeDelta = newSize;

			StartCoroutine (this.executeEnemyHitEffectOnLifeDisplay ());
			StartCoroutine (this.dontTakeDamageForNext(1f));

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
		} else if (other.gameObject.tag == "Objective") {
			locked = true;
			Debug.Log (other.gameObject.tag);

			visate.GetComponent<VisateScript> ().CatchMe (this.gameObject);
		}

		UpdateDisplayLifeColor ();
	}

	void UpdateDisplayLifeColor() {
		Vector2 sizeDisplayLife = this.lifeDisplay.GetComponent<RectTransform> ().sizeDelta;
	
		if (sizeDisplayLife.x <= 48) {
			this.lifeDisplay.color = this.badWealthyColor;
		} 

		if (sizeDisplayLife.x > 48 && sizeDisplayLife.x <= 112) {
			this.lifeDisplay.color = this.mediumWealthyColor;
		}

		if (sizeDisplayLife.x > 112) {
			this.lifeDisplay.color = this.goodWealthyColor;
		}
	}
		
	void PlaySound(AudioClip audio) {
		AudioSource.PlayClipAtPoint (audio, transform.position, 15);
	}

	void PlayMerdaSound(AudioClip audio)
    {
		if (!playingSound) {
			playingSound = true;
			AudioSource.PlayClipAtPoint (audio, transform.position, 15);
			playingSound = false;
		}
    }
		
    private IEnumerator executeEnemyHitEffectOnLifeDisplay() {
		this.lifeDisplay.color = Color.white;
		yield return new WaitForSeconds (.05f);
		this.UpdateDisplayLifeColor ();
	}

	private IEnumerator executeLifeHitEffectOnLifeDisplay() {
		this.lifeDisplay.color = Color.green;
		yield return new WaitForSeconds (.05f);
		this.UpdateDisplayLifeColor ();
	}

	private IEnumerator dontTakeDamageForNext(float seconds) {
		this.canTakeDamage = false;
		yield return new WaitForSeconds (seconds);
		this.canTakeDamage = true;
	}

	private IEnumerator makeDamageEffect() {
		canMakeDamageEffects = false;
		var spriteRender = this.GetComponent<SpriteRenderer> ();
		spriteRender.color = Color.red;
		yield return new WaitForSeconds (.05f);
		spriteRender.color = Color.white;
		canMakeDamageEffects = true;
	}

	public void unlock() {
		locked = false;
	}

	public bool isLocked() {
		return locked;
	}
}