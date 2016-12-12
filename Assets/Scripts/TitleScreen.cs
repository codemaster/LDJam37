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
		public MusicControl musicSystem;

		void Start(){
			musicSystem = GameObject.Find("MusicSystem").GetComponent<MusicControl>();
		}

		/// <summary>
		/// Update in each frame
		/// </summary>
		void Update()
		{
			if (Input.GetButtonDown("Jump"))
			{
				StartCoroutine(LoadGame());
			}
		}

		/// <summary>
		/// Loads the gameplay scene
		/// </summary>
		/// <returns>IEnumerator for coroutine</returns>
		IEnumerator LoadGame()
		{
			musicSystem.InGameMusic (1.0f);
			yield return SceneManager.LoadSceneAsync("Gameplay");
		}
	}
}
