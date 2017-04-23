using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class PlanetView : MonoBehaviour {

	void Start () {
		//gameObject.SetAlpha (0.5f);
		GetComponent<MeshRenderer>().material.color=new Color(Rnd.Float(),Rnd.Float(),Rnd.Float());
	}
	

}
