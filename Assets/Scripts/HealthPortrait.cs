using UnityEngine;

namespace Sparrow
{
	[RequireComponent(typeof(Animator))]
	public class HealthPortrait : SingletonBehaviour<HealthPortrait>
	{
		/// <summary>
		/// The animator component
		/// </summary>
		Animator _animator;

		/// <summary>
		/// When the object is created
		/// </summary>
		void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		/// <summary>
		/// Sets the health for the portrait display
		/// </summary>
		/// <param name="health">Health.</param>
		public void SetHealth(int health)
		{
			if (null != _animator)
			{
				_animator.SetInteger("health", health);
			}
		}
	}
}
