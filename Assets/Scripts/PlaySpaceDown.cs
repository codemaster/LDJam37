using UnityEngine;

namespace Sparrow
{
	[RequireComponent(typeof(FMODUnity.StudioEventEmitter))]
	public class PlaySpaceDown : MonoBehaviour
	{

		// sound emitter reference
		FMODUnity.StudioEventEmitter eventEmitterRef;

		void Awake()
		{
			// Reference to sound emitter component of this object
			eventEmitterRef = GetComponent<FMODUnity.StudioEventEmitter>();
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown("space"))
			{ //if space is pressed down
			  // play sound Bang !
				Debug.Log("Bang!");
				eventEmitterRef.Play();
			}

		}

	}
}
