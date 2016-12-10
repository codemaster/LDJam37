using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySpaceDown : MonoBehaviour {

	// sound emitter reference
	private FMODUnity.StudioEventEmitter eventEmitterRef;

	void Awake(){
		// Reference to sound emitter component of this object
		eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space")){ //if space is pressed down
			// play sound Bang !
			Debug.Log("Bang!");
			eventEmitterRef.Play ();
		}
		
	}
}
