using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sparow
{
	/// <summary>
	/// Title screen.
	/// </summary>

	public class TitleScreen : MonoBehaviour
	{
		//--------------------------------------------------------------------
		// 1: Using the EventRef attribute will present the designer with
		//    the UI for selecting events.
		//--------------------------------------------------------------------
		[FMODUnity.EventRef]
		public string StartSound;

		//--------------------------------------------------------------------
		// 2: Using the EventInstance class will allow us to manage an event
		//    over it's lifetime. Including starting, stopping and changing 
		//    parameters.
		//--------------------------------------------------------------------
		FMOD.Studio.EventInstance startSoundState;


		MusicControl musicSystem;

		bool playerWantsToStart;

		void Start(){
			playerWantsToStart = false;
			musicSystem = GameObject.Find("MusicSystem").GetComponent<MusicControl>();
			startSoundState = FMODUnity.RuntimeManager.CreateInstance(StartSound);
		}

		/// <summary>
		/// Update in each frame
		/// </summary>
		void Update()
		{
			if (Input.GetButtonDown("Jump"))
			{
				musicSystem.InGameMusic (1.0f);
				startSoundState.start ();
				playerWantsToStart = true;

			}

			if (playerWantsToStart && soundFinished()){
				StartCoroutine(LoadGame());
			}
		}


		/// <summary>
		/// Indicates the status of the start sound
		/// </summary>
		/// <returns>True if sound unavaailabe or done, False if sound is still playing</returns>
		bool soundFinished(){
			if (startSoundState == null) {
				return true;
			}
			FMOD.Studio.PLAYBACK_STATE playbackState;
			startSoundState.getPlaybackState(out playbackState);
			if(playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED){
				startSoundState.release();
				startSoundState = null;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Loads the gameplay scene
		/// </summary>
		/// <returns>IEnumerator for coroutine</returns>
		IEnumerator LoadGame()
		{



			yield return SceneManager.LoadSceneAsync("Gameplay");
		}
	}
}
