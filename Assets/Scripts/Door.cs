using UnityEngine;
using UnityEngine.Events;

namespace Sparrow
{
	/// <summary>
	/// Script for handling doors (open, close!)
	/// </summary>
	[RequireComponent(typeof(SpriteRenderer))]
	public class Door : MonoBehaviour
	{
		/// <summary>
		/// The states the door can be in
		/// </summary>
		public enum DoorState
		{
			/// <summary>
			/// When the door is closed
			/// </summary>
			Closed,
			/// <summary>
			/// When the door is open
			/// </summary>
			Open
		}

		/// <summary>
		/// The sprite for when the door is closed
		/// </summary>
		public Sprite ClosedSprite;

		/// <summary>
		/// The sprite for when the door is open
		/// </summary>
		public Sprite OpenSprite;

		/// <summary>
		/// Event type for when a door's state changes
		/// </summary>
		[System.Serializable]
		public class DoorStateChangeEvent : UnityEvent<DoorState> { }

		/// <summary>
		/// When the door's state changes
		/// </summary>
		public DoorStateChangeEvent OnStateChange;

		/// <summary>
		/// The state of the door. Defaults to closed.
		/// </summary>
		public DoorState State { get; private set; }

		/// <summary>
		/// The sprite renderer of the door
		/// </summary>
		protected SpriteRenderer _spriteRenderer;

		/// <summary>
		/// When the object is created
		/// </summary>
		void Awake()
		{
			State = DoorState.Closed;
			_spriteRenderer = GetComponent<SpriteRenderer>();
			UpdateSprite();
		}

		/// <summary>
		/// Opens the door
		/// </summary>
		public void Open()
		{
			// Set the state
			State = DoorState.Open;
			// Change the sprite
			UpdateSprite();
		}

		/// <summary>
		/// Updates the sprite depending on the state of the door
		/// </summary>
		protected void UpdateSprite()
		{
			if (null == _spriteRenderer)
			{
				Debug.LogWarning("Unable to find renderer");
				return;
			}

			_spriteRenderer.sprite = (State == DoorState.Closed)
				? ClosedSprite : OpenSprite;
		}
	}
}
