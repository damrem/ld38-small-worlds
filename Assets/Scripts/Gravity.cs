using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Ext;

public class Gravity : MonoBehaviour {

	public float gravityFactor;

	List<GameObject> attractedList = new List<GameObject> ();

	void Update()
	{
		Fall ();
	}

	void LateUpdate()
	{
		Orient ();
	}

	void Fall () 
	{
		foreach (GameObject attracted in attractedList) {

			Rigidbody2D body = attracted.GetComponent<Rigidbody2D> ();

			Vector2 delta = (transform.position - body.transform.position);

			CircleCollider2D collider = GetComponent<CircleCollider2D> ();
			float radius = collider.radius * Mathf.Sqrt (transform.localScale.x * transform.localScale.x);

			Vector2 attraction = delta.Normalized (radius - delta.magnitude) * 2;

			body.AddForce (attraction * gravityFactor);
		}
	}

	void Orient () {
		foreach (GameObject attracted in attractedList) {

			Rigidbody2D body = attracted.GetComponent<Rigidbody2D> ();

			Vector2 delta = (transform.position - body.transform.position);
			Vector2 normal = delta.GetNormal();

			Quaternion rotation = Quaternion.LookRotation (normal.normalized, -delta.normalized);
			rotation.x = 0;
			rotation.y = 0;

			body.transform.rotation = rotation;

		}
	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag ("Player")) {
			Dbg.Log (this, "OnTriggerEnter2D", collider);
			GameObject hero = collider.gameObject;
			attractedList.Add (hero);
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		
		Dbg.Log (this, "OnTriggerExit2D", collider);

		attractedList.Add (collider.gameObject);


	}
}
