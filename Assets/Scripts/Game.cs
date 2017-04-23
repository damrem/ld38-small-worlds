using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helpers;

public class Game : MonoBehaviour {

	GameObject hero;
	bool isOver;
	Text messageText;

	// Use this for initialization
	void Start () {
		Dbg.AddType<Game> ();
		Dbg.AddType<HeroControl> ();
//		Dbg.AddType<Gravity> ();
//		Dbg.AddType<GravityAttracted> ();

		hero = GameObject.FindGameObjectWithTag ("Player");
		messageText = GameObject.Find ("MessageText").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isOver && hero.GetComponent<GravityAttracted> ().AttractorList.Count == 0)
			GameOver ();


		//if(GetComponentInChildren<GravityAttracted>()
	}

	void GameOver()
	{
		isOver = true;
		Dbg.Log (this, "GameOver");

		StartCoroutine (Fall());

		messageText.text = "Game Over\nPress any key to restart.";
	}

	IEnumerator Fall()
	{
		Dbg.Log (this, "Fall", hero.transform.localScale.x, float.Epsilon);
		while (Mathf.Abs(hero.transform.localScale.x) > 0.01) {
			float intermediateScale = hero.transform.localScale.x * 0.95f;
			Dbg.Log (this, "intermediateScale", intermediateScale);
			hero.transform.localScale = new Vector3 (intermediateScale, intermediateScale, intermediateScale);
			yield return null;
		}
	}
}
