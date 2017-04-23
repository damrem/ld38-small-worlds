using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Game : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Dbg.AddType<HeroControl> ();
//		Dbg.AddType<Gravity> ();
		Dbg.AddType<GravityAttracted> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
