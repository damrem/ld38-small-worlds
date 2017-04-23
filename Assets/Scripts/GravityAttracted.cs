using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

[System.Serializable]
public class GravityAttracted : MonoBehaviour {

	[HideInInspector]
	public GameObject land;

	[HideInInspector]
	public bool ignoreOrientation = false;

	public readonly List<GameObject> attractorList = new List<GameObject> ();

	void FixedUpdate()
	{
		SortAttractorList ();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{

		Dbg.Log (this, "OnTriggerEnter2D", collider);
		if (!collider.CompareTag ("Attractor"))
			return;

		attractorList.Add (collider.gameObject);


	}

	void SortAttractorList()
	{
		attractorList.Sort (SortAttractor2);
	}

	int SortAttractor(GameObject a, GameObject b) {
		float da = (transform.position - a.transform.position).magnitude;
		float db = (transform.position - b.transform.position).magnitude;
		return (int)Mathf.Round ((da - db) * 1000);
	}

	int SortAttractor2(GameObject a, GameObject b){

		float radiusA = a.GetComponent<CircleCollider2D> ().radius;
		float deltaMagnitudeA = (a.transform.position - transform.position).magnitude;
		float factorA = a.GetComponent<Gravity> ().GetFactor (radiusA, deltaMagnitudeA);

		float radiusB = b.GetComponent<CircleCollider2D> ().radius;
		float deltaMagnitudeB = (b.transform.position - transform.position).magnitude;
		float factorB = b.GetComponent<Gravity> ().GetFactor (radiusB, deltaMagnitudeB);

		return (int)Mathf.Round ((factorB - factorA) * 1000);
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if (!collider.CompareTag ("Planet") || !collider.isTrigger)
			return;

		attractorList.Remove (collider.gameObject);


	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag ("Planet") && !collision.collider.isTrigger) {
			land = collision.collider.gameObject;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.CompareTag ("Planet") && !collision.collider.isTrigger) {
			land = null;
		}
	}
}
