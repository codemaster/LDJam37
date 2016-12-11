using UnityEngine;

namespace Sparrow
{
	/// <summary>
	/// Enemy logic
	/// </summary>
    public class Enemy : MonoBehaviour, PowerupNotifee
	{
        /// <summary>
        /// Enemy chase mode.
        /// </summary>
		public enum EnemyChaseMode {Chase, Escape};

        /// <summary>
        /// The chase mode.
        /// </summary>
		public EnemyChaseMode ChaseMode;

		/// <summary>
		/// The movement speed of the character
		/// </summary>
		public float MovementSpeed;

        /// <summary>
        /// Start this instance.
        /// </summary>
        void Start()
        {
            RegisterPowerupNotifee();
        }

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

			// Define and calculate the direction
			Vector3 direction = Vector3.zero;
			switch (ChaseMode)
			{
				// Try to go towards the character
				case EnemyChaseMode.Chase:
					direction = (CharacterController.Instance.transform.position - transform.position).normalized;
					break;
				case EnemyChaseMode.Escape:
					direction = (CharacterController.Instance.transform.position - transform.position).normalized * -1;
					break;
			}


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

        #region PowerupNotifee interface implementations
        /// <summary>
        /// Registers the powerup notifee.
        /// </summary>
        public void RegisterPowerupNotifee()
        {
            CharacterController.Instance.RegisterPowerupNotifee(this);
        }

        /// <summary>
        /// Notifies this object that source has gotten a powerup.
        /// </summary>
        /// <param name="source">Source.</param>
        public void NotifyPowerup(GameObject source)
        {
            ChaseMode = EnemyChaseMode.Escape;
        }

        /// <summary>
        /// Notifies this object that source has gotten a powerdown.
        /// </summary>
        /// <param name="source">Source.</param>
        public void NotifyPowerDown(GameObject source)
        {
            ChaseMode = EnemyChaseMode.Chase;
        }
        #endregion
	}
}
