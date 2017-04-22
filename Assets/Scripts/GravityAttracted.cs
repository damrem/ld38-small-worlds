using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
public class GravityAttracted : MonoBehaviour {

	public readonly List<GameObject> attractorList = new List<GameObject> ();
//	public GameObject ClosestAttractor {
//		get {
//
//			GameObject currentAttractor = null;
//			foreach(GameObject potentialAttractor in attractorList)
//			{
//				if (currentAttractor == null)
//					currentAttractor = potentialAttractor;
//
//				Vector2 currentDelta = transform.position - currentAttractor.transform.position;
//				Vector2 potentialDelta = transform.position - potentialAttractor.transform.position;
//				if (potentialDelta.magnitude < currentDelta.magnitude)
//					currentAttractor = potentialAttractor;
//			}
//			return currentAttractor;
//		}
//	}

	void Update()
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
		attractorList.Sort (SortAttractor);
	}

	int SortAttractor(GameObject a, GameObject b) {
		float da=(transform.position-a.transform.position).magnitude;
		float db=(transform.position-b.transform.position).magnitude;
		return (int)Mathf.Round((da-db)*1000);
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if (!collider.CompareTag ("Planet") || !collider.isTrigger)
			return;

		attractorList.Remove (collider.gameObject);


	}
}
