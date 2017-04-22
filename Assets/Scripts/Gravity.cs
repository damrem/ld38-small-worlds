﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Ext;

public class Gravity : MonoBehaviour {

	public float maxFactor;
	public float minFactor;

	List<GameObject> attractedGameObjectList = new List<GameObject> ();

	public GameObject planetBody;

//	void Update()
//	{
//		Fall ();
//	}

	void Update()
	{
		Orient ();
		Fall ();
	}

	void LateUpdate()
	{
//		Orient ();
	}

	void Fall () 
	{
		foreach (GameObject attractedGameObject in attractedGameObjectList) {

			GravityAttracted gravityAttracted = attractedGameObject.GetComponent<GravityAttracted> ();

			if (gravityAttracted.land != null && gravityAttracted.land != planetBody)
				break;
//			if (gravityAttracted.attractorList[0] != gameObject)
//				break;

			Rigidbody2D attractedBody = attractedGameObject.GetComponent<Rigidbody2D> ();

			Vector2 delta = (transform.position - attractedBody.transform.position);

			CircleCollider2D collider = GetComponent<CircleCollider2D> ();

			float radius = collider.radius * Mathf.Sqrt (transform.localScale.x * transform.localScale.x);

			if (delta.magnitude > radius)
				return;

			Vector2 attraction = delta.Normalized (radius - delta.magnitude);

			float factor = GetFactor (radius, delta.magnitude);
			//float factor = (maxFactor - minFactor) * (radius - delta.magnitude) / radius;
			Dbg.Log (this, "factor", factor);

			attractedBody.AddForce (attraction * factor);

		}
	}

	public float GetFactor(float radius, float deltaMagnitude)
	{
		return (maxFactor - minFactor) * (radius - deltaMagnitude) / radius;
	}

	void Orient () {
		foreach (GameObject attractedGameObject in attractedGameObjectList) {

			GravityAttracted gravityAttracted = attractedGameObject.GetComponent<GravityAttracted> ();
			if (gravityAttracted.attractorList[0] != gameObject)
				break;

			Rigidbody2D attractedBody = attractedGameObject.GetComponent<Rigidbody2D> ();

			Vector2 delta = (transform.position - attractedBody.transform.position);
			Vector2 normal = delta.GetNormal();

			Quaternion destRotation = Quaternion.LookRotation (normal.normalized, -delta.normalized);
			destRotation.x = 0;
			destRotation.y = 0;

			attractedBody.transform.rotation = destRotation;
//			StartCoroutine(SmoothRotate(attractedBody, destRotation));
		}
	}

//	IEnumerator SmoothRotate(Rigidbody2D attractedBody, Quaternion destRotation)
//	{
//		Debug.Log ("sr");
//		Dbg.Log (this, "SmoothRotate", attractedBody, destRotation);
//		float remainingAngle = Quaternion.Angle(attractedBody.transform.rotation, destRotation);
//		Dbg.Log (this, "remainingAngle", remainingAngle);
//		while (remainingAngle > float.Epsilon) {
//			float t = (float)(Time.deltaTime);
//			Dbg.Log (this, "t", t);
//			Quaternion intermediaryRotation = Quaternion.RotateTowards (attractedBody.transform.rotation, destRotation, t);
//			attractedBody.transform.rotation = intermediaryRotation;
//			remainingAngle = Quaternion.Angle (attractedBody.transform.rotation, destRotation);
//			yield return null;
//		}
//	}


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
