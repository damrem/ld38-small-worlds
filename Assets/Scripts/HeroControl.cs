using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Ext;

public class HeroControl : MonoBehaviour {

	public float acceleration = 100f;
	public float maxSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		int h = (int)Input.GetAxisRaw ("Horizontal");
		int v = (int)Input.GetAxisRaw ("Vertical");

		Rigidbody2D body = GetComponent<Rigidbody2D> ();

		Vector2 force = new Vector2 (h, 0);

		force = force.Rotate (body.transform.rotation.eulerAngles.z);
		body.AddForce (force * acceleration);

		if (body.velocity.magnitude > 0) {
			float vComponent = 1 - body.drag;
			Vector2 decelerated=body.velocity.Clone();
			decelerated.x *= vComponent;
			decelerated.y *= vComponent;
			body.velocity = decelerated;
		}

		if (body.velocity.magnitude > maxSpeed) {
			body.velocity = body.velocity.normalized * maxSpeed;
		}


	}
}
