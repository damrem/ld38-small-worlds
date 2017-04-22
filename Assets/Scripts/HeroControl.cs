using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Ext;

public class HeroControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		int h = (int)Input.GetAxisRaw ("Horizontal");
		int v = (int)Input.GetAxisRaw ("Vertical");

//		Dbg.Log (this, "Control", h, v);

		Rigidbody2D body = GetComponent<Rigidbody2D> ();

//		Dbg.Log (this, "rotation", transform.rotation.eulerAngles);

		Vector2 force = new Vector2 (h, v)*10;
		force.Rotate (transform.rotation.eulerAngles.z);
//		force.
//		force = Quaternion.Ang

		body.AddForce (force);

	}
}
