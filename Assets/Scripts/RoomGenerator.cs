using System.Collections.Generic;
using UnityEngine;

namespace Sparrow
{
	/// <summary>
	/// Handles generation of the room
	/// </summary>
	public class RoomGenerator : SingletonBehaviour<RoomGenerator>
	{
		/// <summary>
		/// Pieces of the room/map
		/// </summary>
		public List<RoomPiece> Pieces;

		/// <summary>
		/// The room piece to start with
		/// </summary>
		public RoomPiece StartingRoomPiece;

		/// <summary>
		/// Finds room pieces with paths of a certain direction
		/// </summary>
		/// <returns>The pieces with appropriate paths</returns>
		/// <param name="pathPosition">The needed path</param>
		public List<RoomPiece> PiecesWithPaths(RoomPath.PathPosition pathPosition)
		{
			return Pieces.FindAll((obj) => obj.HasPathDirection(pathPosition));
		}

		/// <summary>
		/// Generates the room!!
		/// </summary>
		void GenerateRoom()
		{
			// Start with the initial piece
			var start = Instantiate(StartingRoomPiece);
			start.transform.SetParent(transform);

			// Keep building out room pieces
			var pathQueue = new Queue<RoomPath>();
			var paths = start.GetPaths();
			foreach (var path in paths)
			{
				pathQueue.Enqueue(path);
			}

			int maxRooms = 10;
			int currentRooms = 1;
			while (pathQueue.Count > 0)
			{
				var path = pathQueue.Dequeue();
				if (null == path || currentRooms >= maxRooms)
				{
					continue;
				}

				var newPaths = CreateConnectedRoomPiece(path);
				++currentRooms;
				foreach (var nextPath in newPaths)
				{
					pathQueue.Enqueue(nextPath);
				}
			}
		}

		/// <summary>
		/// Start of the game!
		/// </summary>
		void Start()
		{
			GenerateRoom();
		}


		/// <summary>
		/// Creates the connected room piece.
		/// </summary>
		/// <returns>The connected room piece.</returns>
		/// <param name="path">New path connectors</param>
		List<RoomPath> CreateConnectedRoomPiece(RoomPath path)
		{
			switch (path.Position)
			{
				case RoomPath.PathPosition.Top:
					{
						// Find another piece that has a top path and spawn it
						var piece = PiecesWithPaths(RoomPath.PathPosition.Bottom);
						if (piece.Count == 0)
						{
							break;
						}
						var index = Random.Range(0, piece.Count);
						var bounds = piece[index].GetBounds();
						var posDelta = (bounds.size.y / 2f) - 1f;
						var newPiece = Instantiate(piece[index],
							(path.transform.position + new Vector3(0f, posDelta, 0f)),
							Quaternion.identity);
						newPiece.transform.SetParent(transform);
						return newPiece.GetPaths().FindAll(
							pathEntry => pathEntry.Position != RoomPath.PathPosition.Bottom);
					}
				case RoomPath.PathPosition.Right:
					{
						// Find another piece that has a left path and spawn it
						var piece = PiecesWithPaths(RoomPath.PathPosition.Left);
						if (piece.Count == 0)
						{
							break;
						}
						var index = Random.Range(0, piece.Count);
						var bounds = piece[index].GetBounds();
						var posDelta = (bounds.size.x / 2f) - 1f;
						var newPiece = Instantiate(piece[index],
							(path.transform.position + new Vector3(posDelta, 0f, 0f)),
							Quaternion.identity);
						newPiece.transform.SetParent(transform);
						return newPiece.GetPaths().FindAll(
							pathEntry => pathEntry.Position != RoomPath.PathPosition.Left);
					}
				case RoomPath.PathPosition.Bottom:
					{
						// Find another piece that has a top path and spawn it
						var piece = PiecesWithPaths(RoomPath.PathPosition.Top);
						if (piece.Count == 0)
						{
							break;
						}
						var index = Random.Range(0, piece.Count);
						var bounds = piece[index].GetBounds();
						var posDelta = (bounds.size.y / 2f) + 1f;
						var newPiece = Instantiate(piece[index],
							(path.transform.position - new Vector3(0f, posDelta, 0f)),
							Quaternion.identity);
						newPiece.transform.SetParent(transform);
						return newPiece.GetPaths().FindAll(
							pathEntry => pathEntry.Position != RoomPath.PathPosition.Top);
					}
				case RoomPath.PathPosition.Left:
					{
						// Find another piece that has a right path and spawn it
						var piece = PiecesWithPaths(RoomPath.PathPosition.Right);
						if (piece.Count == 0)
						{
							break;
						}
						var index = Random.Range(0, piece.Count);
						var bounds = piece[index].GetBounds();
						var posDelta = (bounds.size.x / 2f) + 1f;
						var newPiece = Instantiate(piece[index],
							(path.transform.position - new Vector3(posDelta, 0f, 0f)),
							Quaternion.identity);
						newPiece.transform.SetParent(transform);
						return newPiece.GetPaths().FindAll(
							pathEntry => pathEntry.Position != RoomPath.PathPosition.Right);
					}
				case RoomPath.PathPosition.Ignored:
					// Ignored
					break;
				default:
					Debug.LogWarning("Invalid path position.");
					break;
			}

			// Return null if we weren't able to find or perform anything
			return null;
		}
	}
}
