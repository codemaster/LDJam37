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
                    // TODO
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
                    direction = (chaseTarget.position - transform.position).normalized * -1;
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
                // Should check the neighbours even if the current one is scentless
                //if (node.PlayerScent > 0)
                TargetNode = node.GetSmellierNeighbour();
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
