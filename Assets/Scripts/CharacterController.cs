using UnityEngine;

namespace Sparrow
{
	/// <summary>
	/// Controller for the character
	/// </summary>
	public class CharacterController : SingletonBehaviour<CharacterController>
	{
		/// <summary>
		/// The movement speed of the character
		/// </summary>
		public float MovementSpeed;

		/// <summary>
		/// Updates once per frame
		/// </summary>
		void Update()
		{
			var horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * MovementSpeed;
			var vertical = Input.GetAxis("Vertical") * Time.deltaTime * MovementSpeed;
			transform.Translate(horizontal, vertical, 0f);
		}

		/// <summary>
		/// Validates variables in the Inspector
		/// </summary>
		void OnValidate()
		{
			if (MovementSpeed <= 0f)
			{
				MovementSpeed = 1f;
			}
		}
	}
}
