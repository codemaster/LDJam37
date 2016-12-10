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
		/// If the use key is pressed
		/// </summary>
		protected bool _use;

		/// <summary>
		/// Updates once per frame
		/// </summary>
		void Update()
		{
			// Using something?
			_use = Input.GetButton("Jump");
			
			// Movement
			var horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * MovementSpeed;
			var vertical = Input.GetAxis("Vertical") * Time.deltaTime * MovementSpeed;
			transform.Translate(horizontal, vertical, 0f);
		}

		/// <summary>
		/// When the character is colliding with something else
		/// </summary>
		/// <param name="collision">Collision.</param>
		void OnCollisionStay2D(Collision2D collision)
		{
			var other = collision.gameObject;

			// Check if the other object is a door
			// it is closed and we are trying to 'use' it
			var door = other.GetComponent<Door>();
			if (null != door &&
			    door.State != Door.DoorState.Open &&
			    _use)
			{
				Debug.Log("Open the door");
				// Open the door
				door.Open();
			}
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
