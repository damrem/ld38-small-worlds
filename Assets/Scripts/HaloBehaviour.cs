using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class HaloBehaviour : MonoBehaviour {

	public GameObject haloPrefab;
	Light haloLight;

	// Use this for initialization
	void Start () {
		GameObject haloInstance = Instantiate (haloPrefab, transform, false);

		haloLight = haloInstance.GetComponent<Light> ();
		haloLight.color = new Color (Rnd.Float (0.5f, 1f), Rnd.Float (0.5f, 1f), Rnd.Float (0.5f, 1f));
		haloLight.intensity = 0.25f;
		Vector3 backPos = GetComponentInChildren<Light>().transform.position;
//		backPos.x = 0.1f;
//		backPos.y = 0.1f;
//		haloInstance.transform.position = new Vector3 (0.1f, 0.1f, 1.23f);
		float size = GetComponent<CircleCollider2D> ().radius * 2;
		haloLight.areaSize = new Vector2 (size, size);
		backPos.z = size;
		haloInstance.transform.position = backPos;
	}


	// Update is called once per frame
	void Update () {
		
	}
		
	public void Highlight()
	{
		Dbg.Log (this, "Highlight");
		GetComponentInChildren<Light>().intensity = 0.75f;

	}
}
