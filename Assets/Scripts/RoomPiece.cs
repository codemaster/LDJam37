using System.Collections.Generic;
using UnityEngine;

namespace Sparrow
{
	/// <summary>
	/// Represents a piece to the room
	/// </summary>
	public class RoomPiece : MonoBehaviour
	{
		/// <summary>
		/// Gets all of the paths leading into and out of the room
		/// </summary>
		/// <returns>The paths.</returns>
		public List<RoomPath> GetPaths()
		{
			var paths = new List<RoomPath>();
			paths.AddRange(GetComponentsInChildren<RoomPath>());
			return paths;
		}

		/// <summary>
		/// Checks if this room piece has a path of the appropriate position
		/// </summary>
		/// <returns><c>true</c>, if path direction was hased, <c>false</c> otherwise.</returns>
		/// <param name="pathPosition">Path position.</param>
		public bool HasPathDirection(RoomPath.PathPosition pathPosition)
		{
			var paths = GetPaths();
			foreach (var path in paths)
			{
				path.Position = pathPosition;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Gets the bounds of the entire room piece
		/// </summary>
		/// <returns>The complete bounds of the room piece</returns>
		public Bounds GetBounds()
		{
			var totalBounds = new Bounds();
			var renderers = GetComponentsInChildren<Renderer>();
			foreach (var r in renderers)
			{
				totalBounds.Encapsulate(r.bounds);
			}
			return totalBounds;
		}
	}
}
