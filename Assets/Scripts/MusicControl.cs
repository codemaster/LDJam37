using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour {

	public static MusicControl i;

	[FMODUnity.EventRef]
	public string music = "event:/Music";

	FMOD.Studio.EventInstance musicEv;

	// Use this for initialization
	void Start () {
		if(!i){
			i = this;
			DontDestroyOnLoad (transform.gameObject);
			musicEv = FMODUnity.RuntimeManager.CreateInstance (music);
			musicEv.start ();
		}
		else{
			Destroy (transform.gameObject);
		}

	}

	public void InGameMusic (float Value = 0.0f){
		musicEv.setParameterValue ("InGame", Value);
	}

	public void StressLevel (float Value = 0.0f){
		musicEv.setParameterValue ("Stress", Value);
	}

	public void CreepValue (float Value = 0.0f){
		musicEv.setParameterValue ("Creep", Value);
	}

	Vector3 position;
	
	// Update is called once per frame
	void Update () {
		position = Camera.main.transform.position;
		position.z = 10;
		musicEv.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes (position));
	}
}
