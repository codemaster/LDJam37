using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour {

	[FMODUnity.EventRef]
	public string music = "event:/Music";

	FMOD.Studio.EventInstance musicEv;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
		musicEv = FMODUnity.RuntimeManager.CreateInstance (music);
		musicEv.start ();
	}

	public void InGameMusic (float Value = 0.0f){
		musicEv.setParameterValue ("InGame", Value);
	}

	public void StressLevel (float Value = 0.0f){
		musicEv.setParameterValue ("Stress", Value);
	}

	public void SpeedLevel (float Value = 0.0f){
		musicEv.setParameterValue ("Speed", Value);
	}

	public void CreepValue (float Value = 0.0f){
		musicEv.setParameterValue ("Creep", Value);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
