using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helpers;
using Ext;
using UnityEngine.Events;

public class HeroControl : MonoBehaviour {

	public float acceleration;
	public float maxSpeed;

	public float jumpFactor;

	public int maxBoost;
	int remainingBoost;
//	int currentBoost = 0;

	public int nbNoJump;
	int currentNoJump;

	public UnityEvent moved = new UnityEvent();

	public AudioClip[] jumpSounds;
	public AudioClip[] landSounds;

//	public bool hasLanded;

//	int boost = 100;
//	bool canBoost = false;
	public float boostIncreaseDelay = 10.0f;
	float nextBoostTime = 0.0f;

	public Text boostText;

	GravityAttracted attractedComponent;
	Animator animator;

	void Start()
	{
		attractedComponent = GetComponent<GravityAttracted> ();
		animator = GetComponentInChildren<Animator> ();
	}

	void Update()
	{
		boostText.text = remainingBoost + "/" + maxBoost;
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (Time.time > nextBoostTime && remainingBoost < maxBoost) {
			//			canBoost = true;
			remainingBoost++;
			nextBoostTime = Time.time + boostIncreaseDelay;
		}

		int h = (int)Input.GetAxisRaw ("Horizontal");
		int v = (int)Input.GetAxisRaw ("Vertical");

		Rigidbody2D body = GetComponent<Rigidbody2D> ();

		bool hasMoved = false;

		if (h != 0) {

			hasMoved = true;
			
			Dbg.Log (this, "attractors", attractedComponent.AttractorList.Count);

			transform.localScale = new Vector3 (-Mathf.Abs (transform.localScale.x) * h, transform.localScale.y, transform.localScale.z);

			animator.SetTrigger("startWalking");

			Dbg.Log (this, "animator", animator);


			if (attractedComponent.AttractorList.Count > 0) {

				Vector2 walkForce = new Vector2 (h, 0);

				walkForce = walkForce.Rotate (body.transform.rotation.eulerAngles.z);
				body.AddForce (walkForce * acceleration);
			} else {
				body.AddTorque (-(float)h);
			}
		} else {
			animator.SetTrigger("stopWalking");
		}

		if (body.velocity.magnitude > 0) {
			if (body.velocity.magnitude > maxSpeed) {
				body.velocity = body.velocity.normalized * maxSpeed;
			}

			float vComponent = 1 - body.drag;
			Vector2 decelerated = body.velocity.Clone ();
			decelerated.x *= vComponent;
			decelerated.y *= vComponent;
			body.velocity = decelerated;
		}

//		if (v > 0 && (GetComponent<GravityAttracted>().land || currentJumpLevel < nbJumpLevels)) {
//			currentJumpLevel++;
//			Vector2 jumpForce = new Vector2 (0, jumpFactor);
//			Dbg.Log (this, "jumpForce", jumpForce.magnitude);
//			jumpForce = jumpForce.Rotate (body.transform.rotation.eulerAngles.z);
//			Dbg.Log (this, "jumpForce", jumpForce.magnitude);
//			body.AddForce (jumpForce);
//		}

		if (v > 0){

			hasMoved = true;
			hasJumped = true;

			if(attractedComponent.AttractorList.Count>1)
				attractedComponent.ignoreOrientation = true;

			if (attractedComponent.land != null){
				AudioSource source = GetComponent<AudioSource> ();
				source.clip = jumpSounds [Rnd.Int (0, jumpSounds.Length)];
				source.pitch = Rnd.Float (0.95f, 1.05f);
				source.Play ();
			}
//				SoundManager.instance.RandomizeSfx (jumpSounds);

			if(remainingBoost > 0) {
	//			currentBoost++;
	//			Dbg.Log (this, "BOOST", remainingBoost);
				remainingBoost--;
				Vector2 boostForce = new Vector2 (0, jumpFactor);
	//			Dbg.Log (this, "boostForce", boostForce.magnitude);
				boostForce = boostForce.Rotate (body.transform.rotation.eulerAngles.z);
	//			Dbg.Log (this, "boostForce", boostForce.magnitude);
				body.AddForce (boostForce);
			}
		} else
			attractedComponent.ignoreOrientation = false;

		if (hasMoved)
			moved.Invoke ();
	}

	bool hasJumped = false;

	void OnCollisionEnter2D(Collision2D collision)
	{
		
		if (collision.collider.CompareTag ("Planet")) {

			if (hasJumped) {
				hasJumped = false;
				AudioSource source = GetComponent<AudioSource> ();
				source.clip = landSounds [Rnd.Int (0, landSounds.Length)];
				source.pitch = Rnd.Float (0.95f, 1.05f);
				source.Play ();
			}
////			Dbg.Log (this, "collide", collision, collision.collider.tag);
//			//hasLanded = true;
////			boost = 0;
//			currentNoJump = 0;
		}
	}



//	void OnCollisionExit2D(Collision2D collision)
//	{
//		Dbg.Log (this, "collide", collision, collision.collider.tag);
//		if (collision.collider.CompareTag ("PlanetBody"))
////			canJump = false;
//	}

}
