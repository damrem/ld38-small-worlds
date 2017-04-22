using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Ext;

public class HeroControl : MonoBehaviour {

	public float acceleration;
	public float maxSpeed;

	public float jumpFactor;

	public int nbJumpLevels;
	int currentJumpLevel;

	public int nbNoJump;
	int currentNoJump;

//	public bool hasLanded;



	// Update is called once per frame
	void FixedUpdate () {

		int h = (int)Input.GetAxisRaw ("Horizontal");
		int v = (int)Input.GetAxisRaw ("Vertical");

		Rigidbody2D body = GetComponent<Rigidbody2D> ();

		Vector2 walkForce = new Vector2 (h, 0);

		walkForce = walkForce.Rotate (body.transform.rotation.eulerAngles.z);
		body.AddForce (walkForce * acceleration);

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

		if (v > 0 && (GetComponent<GravityAttracted>().land || currentJumpLevel < nbJumpLevels)) {
			currentJumpLevel++;
			Vector2 jumpForce = new Vector2 (0, jumpFactor);
			Dbg.Log (this, "jumpForce", jumpForce.magnitude);
			jumpForce = jumpForce.Rotate (body.transform.rotation.eulerAngles.z);
			Dbg.Log (this, "jumpForce", jumpForce.magnitude);
			body.AddForce (jumpForce);
		}


	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag ("Planet") && !collision.collider.isTrigger) {
			Dbg.Log (this, "collide", collision, collision.collider.tag);
			//hasLanded = true;
			currentJumpLevel = 0;
			currentNoJump = 0;
		}
	}



//	void OnCollisionExit2D(Collision2D collision)
//	{
//		Dbg.Log (this, "collide", collision, collision.collider.tag);
//		if (collision.collider.CompareTag ("PlanetBody"))
////			canJump = false;
//	}

}
