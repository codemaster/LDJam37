using System.Collections;
using UnityEngine;

namespace Sparrow
{
	/// <summary>
	/// Enemy spawner.
	/// </summary>
	public class EnemySpawner : MonoBehaviour
	{
		/// <summary>
		/// The number of enemies to spawn in this spawner
		/// </summary>
		public int NumberOfEnemies;

		/// <summary>
		/// The delay between spawning each enemy
		/// </summary>
		public float DelayBetweenSpawns;

		/// <summary>
		/// The prefab to spawn
		/// </summary>
		public Transform EnemyPrefab;

		/// <summary>
		/// Init
		/// </summary>
		void Start()
		{
			Debug.Log(EnemyPrefab.GetType());
			StartCoroutine(HandleSpawn());
		}

		/// <summary>
		/// Coroutine that handles spawning
		/// </summary>
		/// <returns>IEnumerator for coroutine</returns>
		IEnumerator HandleSpawn()
		{
			// If we have enemies left to spawn
			while (NumberOfEnemies > 0)
			{
				SpawnEnemy();
				--NumberOfEnemies;
				yield return new WaitForSeconds(DelayBetweenSpawns);
			}

			yield break;
		}

		/// <summary>
		/// Spawns an enemy
		/// </summary>
		protected void SpawnEnemy()
		{
			if (null == EnemyPrefab)
			{
				Debug.LogWarning("No enemy prefab defined for spawner " + name);
				return;
			}

			Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
		}
	}
}
