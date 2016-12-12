using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sparrow
{
	/// <summary>
	/// Gameplay screen.
	/// </summary>
	public class GameplayScreen : MonoBehaviour
	{
		/// <summary>
		/// The number of collectables in this level
		/// </summary>
		int _numCollectables;

		/// <summary>
		/// The number of collectables currently obtained
		/// </summary>
		int _collected;

		/// <summary>
		/// The next level to load
		/// </summary>
		public string NextLevel;

		/// <summary>
		/// If defeated, load this level
		/// </summary>
		public string Defeat;

		/// <summary>
		/// Initialization
		/// </summary>
		void Start()
		{
			_numCollectables = FindObjectsOfType<CollectItem>().Length;
		}

		/// <summary>
		/// When health changes
		/// </summary>
		/// <param name="health">Health.</param>
		public void OnHealthChange(int health)
		{
			if (health <= 0)
			{
				// Load Defeat!
				StartCoroutine(LoadLevel(Defeat));
			}
		}

		/// <summary>
		/// When an item is collected
		/// </summary>
		public void OnCollectItem()
		{
			if (++_collected >= _numCollectables)
			{
				// Load next level
				StartCoroutine(LoadLevel(NextLevel));
			}
		}

		/// <summary>
		/// Loads the level.
		/// </summary>
		/// <returns>IEnumerator to utilize for coroutine</returns>
		/// <param name="level">Level name</param>
		protected IEnumerator LoadLevel(string level)
		{
			yield return SceneManager.LoadSceneAsync(level);
		}
	}
}
