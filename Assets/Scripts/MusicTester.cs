using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTester : MonoBehaviour {

	public MusicControl musicSystem;

	public float inGame = 0.0f;
	public float stress = 0.0f;
	public float creep = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		musicSystem.InGameMusic (inGame);
		musicSystem.StressLevel (stress);
		musicSystem.CreepValue (creep);
		
	}
}
