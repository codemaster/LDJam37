using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicTrigger : MonoBehaviour {

	public float Stress;

	MusicControl musicSystem;
	// Use this for initialization
	void Start () {
		musicSystem = GameObject.Find("MusicSystem").GetComponent<MusicControl>();
		musicSystem.StressLevel (Stress);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
