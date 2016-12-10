using UnityEngine;

namespace Sparrow
{
	/// <summary>
	/// Enemy logic
	/// </summary>
	public class Enemy : MonoBehaviour
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
			// Check if we can move
			if (MovementSpeed <= 0f)
			{
				return;
			}

			// Try to go towards the character
			var direction = (CharacterController.Instance.transform.position - transform.position).normalized;
			var movement = Time.deltaTime * MovementSpeed * direction;
			transform.Translate(movement);
		}

		/// <summary>
		/// Validates variables in the Inspector
		/// </summary>
		void OnValidate()
		{
			// Ensure the enemy is not faster than the character
			if (CharacterController.Instance.MovementSpeed <= MovementSpeed)
			{
				Debug.LogWarning("Movement speed of " + name + " is equal or faster than the character!");
			}
		}
	}
}
