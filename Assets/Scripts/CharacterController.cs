using UnityEngine;
using System.Collections.Generic;

namespace Sparrow
{
	/// <summary>
	/// Controller for the character
	/// </summary>
    public class CharacterController : SingletonBehaviour<CharacterController>, PowerupNotifier
	{
		/// <summary>
		/// The movement speed of the character
		/// </summary>
		public float MovementSpeed;

        /// <summary>
        /// Is the player PoweredUp?
        /// </summary>
        [SerializeField]
        private bool _poweredUp;
        public bool PoweredUp
        {
            get
            {
                return _poweredUp;
            }
            set
            {
                _poweredUp = value;
                if (value)
                {
                    NotifyPowerup();
                    LastPowerUpTime = Time.realtimeSinceStartup;
                }
                else
                    NotifyPowerDown();
            }
        }

        /// <summary>
        /// How long the powerup lasts.
        /// </summary>
        public float PowerUpTime;

        /// <summary>
        /// The last power up time.
        /// </summary>
        private float LastPowerUpTime = 0;

        /// <summary>
        /// The powerup notifees.
        /// </summary>
        private List<PowerupNotifee> _powerupNotifees;

        /// <summary>
        /// Start this instance.
        /// </summary>
        void Start()
        {
            _powerupNotifees = new List<PowerupNotifee>();
        }

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

            CheckPowerUpTime();
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

        /// <summary>
        /// Checks the power up time.
        /// </summary>
        private void CheckPowerUpTime()
        {
            if (PoweredUp)
            {
                if (Time.realtimeSinceStartup - LastPowerUpTime > PowerUpTime)
                    PoweredUp = false;
            }
        }

        #region PowerupNotifier interface implementation
        /// <summary>
        /// Registers new powerup notifees.
        /// </summary>
        /// <param name="notifee">Notifee.</param>
        public void RegisterPowerupNotifee(PowerupNotifee notifee)
        {
            if (_powerupNotifees == null)
            {
                Debug.LogWarning(name + "'s CharacterController's powerupNotifees list not instantiated. Check the Scripts Execution Order.");
                return;
            }
            _powerupNotifees.Add(notifee);
        }

        /// <summary>
        /// Notifies powerup event to all registered notifees.
        /// </summary>
        public void NotifyPowerup()
        {
            foreach (var notifee in _powerupNotifees)
            {
                notifee.NotifyPowerup(this.gameObject);
            }
        }

        /// <summary>
        /// Notifies powerdown event to all registered notifees.
        /// </summary>
        public void NotifyPowerDown()
        {
            foreach (var notifee in _powerupNotifees)
            {
                notifee.NotifyPowerDown(this.gameObject);
            }
        }
        #endregion

        /// <summary>
        /// Raises the trigger enter2 d event.
        /// </summary>
        /// <param name="col">Col.</param>
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Powerup"))
                PoweredUp = true;
        }
	}
}
