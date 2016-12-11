using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Sparrow
{
	/// <summary>
	/// Class the stores sorted scores
	/// </summary>
	public class Scoreboard : Singleton<Scoreboard>
	{
		/// <summary>
		/// The maximum number of scores to keep in data
		/// </summary>
		protected const int MaxScores = 10;

		/// <summary>
		/// Where the scoreboard data is stored
		/// </summary>
		protected static string DataFile = Application.persistentDataPath + "/scoreboard.json";
		
		/// <summary>
		/// List of capped scores
		/// </summary>
		protected readonly List<ulong> _scores = new List<ulong>();

		/// <summary>
		/// Retrieves the highest score from the listing
		/// </summary>
		/// <value>The high score.</value>
		public ulong HighScore { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Sparrow.Scoreboard"/> class.
		/// </summary>
		public Scoreboard()
		{
			LoadScoreboard();
		}

		/// <summary>
		/// Adds a score to the scoreboard
		/// </summary>
		/// <param name="score">Score.</param>
		public void AddScore(ulong score)
		{
			lock (_scores)
			{
				// Add the new score and sort the listing
				_scores.Add(score);
				// Sort (reverse)
				_scores.Sort((x, y) => y.CompareTo(x));

				// Prune anything over the maximum number we want to store
				if (_scores.Count > MaxScores)
				{
					_scores.RemoveRange(MaxScores + 1,
						_scores.Count - MaxScores);
				}

				HighScore = _scores[0];
			}

			// Save the scoreboard
			SaveScoreboard();
		}

		/// <summary>
		/// Loads scoreboard data from disk
		/// </summary>
		protected void LoadScoreboard()
		{
			if (!File.Exists(DataFile))
			{
				Debug.Log("Scoreboard data file doesn't exist, not loading");
				return;
			}
			
			var data = File.ReadAllText(DataFile);
			if (string.IsNullOrEmpty(data))
			{
				Debug.LogWarning("Empty scoreboard data file, not loading.");
				return;
			}

			lock (_scores)
			{
				var scores = JsonUtility.FromJson<List<ulong>>(data);
				_scores.Clear();
				_scores.AddRange(scores);
			}
		}

		/// <summary>
		/// Saves scoreboard data to disk
		/// </summary>
		protected void SaveScoreboard()
		{
			string data = null;
			lock (_scores)
			{
				data = JsonUtility.ToJson(_scores);
			}

			if (string.IsNullOrEmpty(data))
			{
				Debug.LogWarning("Empty scoreboard data, not writing to disk.");
				return;
			}

			File.WriteAllText(DataFile, data);
		}
	}
}
