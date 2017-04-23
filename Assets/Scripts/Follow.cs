using Ext;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	public GameObject target;

	void LateUpdate () {

		Vector2 delta = target.transform.position - transform.position;

		Rigidbody2D body = GetComponent<Rigidbody2D> ();
		body.velocity = delta;
	}
}
