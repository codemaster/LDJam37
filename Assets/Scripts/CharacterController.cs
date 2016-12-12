﻿using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Sparrow
{
	/// <summary>
	/// Controller for the character
	/// </summary>
	[RequireComponent(typeof(Animator))]
    public class CharacterController : SingletonBehaviour<CharacterController>, PowerupNotifier
	{
		/// <summary>
		/// Movement directions
		/// </summary>
		enum MovementDirection
		{
			Up,
			Right,
			Down,
			Left
		}
		
		/// <summary>
		/// The movement speed of the character
		/// </summary>
		public float MovementSpeed;

		/// <summary>
		/// The player's current health accessor/setter
		/// </summary>
		public int Health
		{
			get
			{
				return _health;
			}
			private set
			{
				_health = value;
				OnHealthChange.Invoke(Health);
			}
		}

		/// <summary>
		/// The player's starting health
		/// </summary>
		public int StartingHealth;

		/// <summary>
		/// Event class for health changing
		/// </summary>
		[Serializable]
		public class HealthChangeEvent : UnityEvent<int> { };

		/// <summary>
		/// Event for when the character's health changes
		/// </summary>
		public HealthChangeEvent OnHealthChange;

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
		/// The animator for the character
		/// </summary>
		Animator _animator;

		/// <summary>
		/// The current health of the character
		/// </summary>
		int _health;

        /// <summary>
        /// Start this instance.
        /// </summary>
        void Start()
        {
			Health = StartingHealth;
            _powerupNotifees = new List<PowerupNotifee>();
			_animator = GetComponent<Animator>();
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
			var vertical = Input.GetAxis("Vertical") * Time.deltaTime * MovementSpeed;
			var horizontal = (Mathf.Abs(vertical) > Mathf.Epsilon) ? 0f :
				Input.GetAxis("Horizontal") * Time.deltaTime * MovementSpeed;
			UpdateMovementSprite(vertical, horizontal);
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

			// Check if the other object is an enemy
			var enemy = other.GetComponent<Enemy>();
			if (null != enemy)
			{
				Debug.Log("Attacked by enemy");
				// Destroy the enemy
				Destroy(other);
				// Reduce health by 1
				Health--;
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

		/// <summary>
		/// Updates the movement sprite.
		/// </summary>
		/// <param name="vertical">Vertical movement</param>
		/// <param name="horizontal">Horizontal movement</param>
		void UpdateMovementSprite(float vertical, float horizontal)
		{
			// Prioritize vertical movement
			if (Mathf.Abs(vertical) > 0f)
			{
				// Going Up
				if (vertical > 0f)
				{
					_animator.SetInteger("direction", (int)MovementDirection.Up);
				}

				// Going Down
				if (vertical < 0f)
				{
					_animator.SetInteger("direction", (int)MovementDirection.Down);
				}
				return;
			}

			// Going Right
			if (horizontal > 0f)
			{
				_animator.SetInteger("direction", (int)MovementDirection.Right);
			}

			// Going Left
			if (horizontal < 0f)
			{
				_animator.SetInteger("direction", (int)MovementDirection.Left);
			}
		}
	}
}
