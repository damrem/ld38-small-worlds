using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class HaloBehaviour : MonoBehaviour {

	public GameObject haloPrefab;

	// Use this for initialization
	void Start () {
		GameObject haloInstance = Instantiate (haloPrefab, transform, false);

		Light light = haloInstance.GetComponent<Light> ();
		light.color = new Color (Rnd.Float (0.5f, 1f), Rnd.Float (0.5f, 1f), Rnd.Float (0.5f, 1f));
		light.intensity = Rnd.Float (0.25f, 0.5f);
		Vector3 backPos = light.transform.position;
//		backPos.x = 0.1f;
//		backPos.y = 0.1f;
//		haloInstance.transform.position = new Vector3 (0.1f, 0.1f, 1.23f);
		float size = GetComponent<CircleCollider2D> ().radius * 2;
		light.areaSize = new Vector2 (size, size);
		backPos.z = size;
		haloInstance.transform.position = backPos;
	}


	// Update is called once per frame
	void Update () {
		
	}
}
