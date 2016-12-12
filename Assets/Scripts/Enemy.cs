using UnityEngine;

namespace Sparrow
{
	/// <summary>
	/// Enemy logic
	/// </summary>
	[RequireComponent(typeof(Animator))]
    public class Enemy : MonoBehaviour, PowerupNotifee
	{
        /// <summary>
        /// Enemy chase mode.
        /// </summary>
		public enum EnemyChaseMode {Chase, Escape};

        /// <summary>
        /// Movement type.
        /// </summary>
        public enum EnemyMovementType {DirectChase, ScentTracking, FreeRoam};

        /// <summary>
        /// The chase mode.
        /// </summary>
		public EnemyChaseMode ChaseMode;

        /// <summary>
        /// The type of the movement.
        /// </summary>
        public EnemyMovementType MovementType;

        /// <summary>
        /// The target node.
        /// </summary>
        public ScentNode TargetNode;

		/// <summary>
		/// The movement speed of the character
		/// </summary>
		public float MovementSpeed;

		/// <summary>
		/// The animator for the enemy
		/// </summary>
		Animator _animator;

        /// <summary>
        /// Start this instance.
        /// </summary>
        void Start()
        {
			_animator = GetComponent<Animator>();
            RegisterPowerupNotifee();
        }

		/// <summary>
		/// Updates once per frame
		/// </summary>
		void Update()
		{
            ProcessMovement();
		}

        /// <summary>
        /// Processes the movement.
        /// </summary>
        private void ProcessMovement()
        {
            // Check if we can move
            if (MovementSpeed <= 0f)
            {
                return;
            }

            switch (MovementType)
            {
                case EnemyMovementType.DirectChase:
                    DirectChase(CharacterController.Instance.transform);
                    break;
                case EnemyMovementType.FreeRoam:
                    if(TargetNode != null)
                        DirectChase(TargetNode.transform);
                    break;
                case EnemyMovementType.ScentTracking:
                    if(TargetNode != null)
                        DirectChase(TargetNode.transform);
                    break;
            }
        }

        /// <summary>
        /// Performs a direct chase after the player without checking for obstacles.
        /// </summary>
        private void DirectChase(Transform chaseTarget)
        {
            // Define and calculate the direction
            Vector3 direction = Vector3.zero;
            switch (ChaseMode)
            {
                // Try to go towards the character
                case EnemyChaseMode.Chase:
                    direction = (chaseTarget.position - transform.position).normalized;
                    break;
                case EnemyChaseMode.Escape:
                    // The escaping system works by taking following the nodes with the least amount of scent,
                    // wich is not optimal, to say the least. Doing it like this, then we don't have to process the opposite direction here.
                    // Leaving this case here to come back to it later and do all this properly.
                    direction = (chaseTarget.position - transform.position).normalized;// * -1;
                    break;
            }

            var movement = Time.deltaTime * MovementSpeed * direction;
			UpdateSprite(movement);
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

        /// <summary>
        /// Raises the trigger enter2 d event.
        /// </summary>
        /// <param name="col">Col.</param>
        void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("ScentNode"))
            {
                // This GetComponent might slow things down a bit
                ScentNode node = col.gameObject.GetComponent<ScentNode>();
                if (node.PlayerScent > 0)
                    MovementType = EnemyMovementType.ScentTracking;
                else
                    MovementType = EnemyMovementType.FreeRoam;
                
                switch (MovementType)
                {
                    case EnemyMovementType.ScentTracking:
                        switch (ChaseMode)
                        {
                            case EnemyChaseMode.Chase:
                                TargetNode = node.GetSmellierNeighbour();
                                break;

                            case EnemyChaseMode.Escape:
                                TargetNode = node.GetLessSmellyNeighbor();
                                break;
                        }
                        break;

                    case EnemyMovementType.FreeRoam:
                        if(TargetNode == null || TargetNode == node)
                            TargetNode = node.GetRandomNeighbor();
                        break;
                }
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

		/// <summary>
		/// Updates the animation of the sprite
		/// </summary>
		/// <param name="direction">Direction that the enemy is moving</param>
		void UpdateSprite(Vector3 direction)
		{
			// Handle vertical first
			if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
			{
				if (direction.y > 0f)
				{
					// Up
					_animator.SetInteger("direction", (int)MovementDirection.Up);
				}

				if (direction.y < 0f)
				{
					// Down
					_animator.SetInteger("direction", (int)MovementDirection.Down);
				}
				return;
			}

			// Handle horizontal
			if (direction.x < 0f)
			{
				// Left
				_animator.SetInteger("direction", (int)MovementDirection.Left);
			}

			if (direction.x > 0f)
			{
				// Right
				_animator.SetInteger("direction", (int)MovementDirection.Right);
			}
		}
	}
}
