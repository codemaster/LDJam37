using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sparow
{
	/// <summary>
	/// Title screen.
	/// </summary>
	public class EndGameScreen : MonoBehaviour
	{
		/// <summary>
		/// Update in each frame
		/// </summary>

		MusicControl musicSystem;

		void Start(){
			musicSystem = GameObject.Find("MusicSystem").GetComponent<MusicControl>();
			musicSystem.InGameMusic (0.0f);
		}

		void Update()
		{
			if (Input.GetButtonDown("Jump"))
			{
				StartCoroutine(LoadTitle());
			}
		}

		/// <summary>
		/// Loads the title scene
		/// </summary>
		/// <returns>IEnumerator for coroutine</returns>
		IEnumerator LoadTitle()
		{
			yield return SceneManager.LoadSceneAsync("TitleScreen");
		}
	}
}
