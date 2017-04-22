using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Ext;

public class Gravity : MonoBehaviour {

	public float maxFactor;
	public float minFactor;

	List<GameObject> attractedGameObjectList = new List<GameObject> ();

//	void Update()
//	{
//		Fall ();
//	}

	void Update()
	{
		Fall ();
		Orient ();
	}

	void Fall () 
	{
		foreach (GameObject attractedGameObject in attractedGameObjectList) {

//			GravityAttracted gravityAttracted = attractedGameObject.GetComponent<GravityAttracted> ();
//			if (gravityAttracted.attractorList[0] != gameObject)
//				break;

			Rigidbody2D body = attractedGameObject.GetComponent<Rigidbody2D> ();

			Vector2 delta = (transform.position - body.transform.position);

			CircleCollider2D collider = GetComponent<CircleCollider2D> ();

			float radius = collider.radius * Mathf.Sqrt (transform.localScale.x * transform.localScale.x);

			if (delta.magnitude > radius)
				return;

			Vector2 attraction = delta.Normalized (radius - delta.magnitude);

			float factor = (maxFactor - minFactor) * (radius-delta.magnitude) / radius;
			Dbg.Log (this, "factor", factor);

			body.AddForce (attraction * factor);

		}
	}

	void Orient () {
		foreach (GameObject attractedGameObject in attractedGameObjectList) {

			GravityAttracted gravityAttracted = attractedGameObject.GetComponent<GravityAttracted> ();
			if (gravityAttracted.attractorList[0] != gameObject)
				break;

			Rigidbody2D body = attractedGameObject.GetComponent<Rigidbody2D> ();

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
			attractedGameObjectList.Add (hero);
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		
		Dbg.Log (this, "OnTriggerExit2D", collider);

		attractedGameObjectList.Remove (collider.gameObject);


	}
}
