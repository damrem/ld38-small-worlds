using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Ext;

public class Gravity : MonoBehaviour {

	public float gravityFactor = 10.0f;

	List<GameObject> attractedList=new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		foreach (GameObject attracted in attractedList) {
			Rigidbody2D body = attracted.GetComponent<Rigidbody2D> ();
			Vector2 delta = (transform.position - body.transform.position);



			Vector2 normal = delta.GetNormal();

			Dbg.Log (this, "delta", delta);
			Quaternion rotation;
//			rotation = Quaternion.AngleAxis(0, normal);
//			Dbg.Log (this, "rotation", rotation);
//			rotation = Quaternion.Euler (normal);

			rotation = Quaternion.LookRotation (normal.normalized, -delta.normalized);
			rotation.x = 0;
			rotation.y = 0;

//			Quaternion.


//			attracted.transform.Rotate(normal, 0);
			Dbg.Log (this, "rotationz", rotation);
			body.transform.rotation = rotation;
			body.AddForce (delta * gravityFactor);
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
