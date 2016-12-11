using UnityEngine;

namespace Sparrow
{
	/// <summary>
	/// A path that can connect two rooms
	/// </summary>
	public class RoomPath : MonoBehaviour
	{
		/// <summary>
		/// The position of the path
		/// </summary>
		public enum PathPosition
		{
			/// <summary>
			/// Ignore this path
			/// </summary>
			Ignored,
			/// <summary>
			/// Path is at the top of the room piece
			/// </summary>
			Top,
			/// <summary>
			/// Path is at the right side of the room piece
			/// </summary>
			Right,
			/// <summary>
			/// Path is at the bottom of the room piece
			/// </summary>
			Bottom,
			/// <summary>
			/// Path is at the left side of the room piece
			/// </summary>
			Left
		};

		/// <summary>
		/// The position of this room path
		/// </summary>
		public PathPosition Position = PathPosition.Ignored;
	}
}
