using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Gravity : MonoBehaviour {

	public float gravityFactor = 10.0f;

	List<GameObject> attractedList=new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject attracted in attractedList) {
			Rigidbody2D body = attracted.GetComponent<Rigidbody2D> ();
			Vector2 force = (transform.position - attracted.transform.position) * gravityFactor;
			Dbg.Log (this, "force", force);
			body.AddForce (force);
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
