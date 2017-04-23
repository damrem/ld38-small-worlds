using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helpers;
using Ext;

public class Game : MonoBehaviour {

	GameObject hero;
	HeroControl heroControl;
	GravityAttracted heroGravityAttracted;
	public static bool isOver;
	Text messageText;

	public AudioClip fallSound;
	public AudioSource sfxSource;

	// Use this for initialization
	void Awake() {
		Dbg.AddType<Game> ();
		Dbg.AddType<HeroControl> ();
//		Dbg.AddType<Gravity> ();
//		Dbg.AddType<GravityAttracted> ();

		Dbg.Log (this, "Awake");
	}

	PlanetView[] planets;
	void Start()
	{
		Dbg.Log (this, "Start");

		hero = GameObject.FindGameObjectWithTag ("Player");
		messageText = GameObject.Find ("MessageText").GetComponent<Text> ();

		messageText.text = "Small Worlds!\n@damrem\n#ld38\n\nUse arrow keys to move.";

		heroControl = hero.GetComponent<HeroControl> ();
		heroControl.moved.AddListener (ClearMessage);

		heroGravityAttracted = hero.GetComponent<GravityAttracted> ();

		sfxSource = GetComponent<AudioSource> ();

		planets = GetComponentsInChildren<PlanetView> ();

		isOver = false;
	}

	void ClearMessage()
	{
		heroControl.moved.RemoveListener (ClearMessage);
		messageText.text = "";
	}

	// Update is called once per frame
	void LateUpdate () {
		
		if (!isOver && heroGravityAttracted.AttractorList.Count == 0)
			GameOver ();

//		Dbg.Log (this, "planets", planets.Length);

		PlanetView[] visitedPlanets = planets.Filter (delegate(PlanetView t) {
			return t.hasBeenVisited;
		});

//		Dbg.Log(this, "visiteds", visitedPlanets.Length);
		//if(GetComponentInChildren<GravityAttracted>()

		if (planets.Length == visitedPlanets.Length)
			Victory ();
	}

	void GameOver()
	{
		isOver = true;
		Dbg.Log (this, "GameOver");

		StartCoroutine (Fall());
		sfxSource.clip = fallSound;
		sfxSource.Play ();

		messageText.text = "Game Over\nPress any key to restart.";
	}

	void Victory()
	{
		isOver = true;
		messageText.text = "You visited all the Small Worlds for now!\nHope you enjoyed.\nMade in 48h by @damrem for #ld48.";
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
