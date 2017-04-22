using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttracted : MonoBehaviour {

	List<GameObject> attractorList = new List<GameObject> ();
	public GameObject ClosestAttractor {
		get {

			GameObject currentAttractor = null;
			foreach(GameObject potentialAttractor in attractorList)
			{
				if (currentAttractor == null)
					currentAttractor = potentialAttractor;

				Vector2 currentDelta = transform.position - currentAttractor.transform.position;
				Vector2 potentialDelta = transform.position - potentialAttractor.transform.position;
				if (potentialDelta.magnitude < currentDelta.magnitude)
					currentAttractor = potentialAttractor;
			}
			return currentAttractor;
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (!collider.CompareTag ("Attractor"))
			return;

		attractorList.Add (collider.gameObject);


	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if (!collider.CompareTag ("Attractor"))
			return;

		attractorList.Remove (collider.gameObject);
	}
}
