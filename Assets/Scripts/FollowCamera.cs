using UnityEngine;

namespace Sparrow
{
	/// <summary>
	/// Script to have the camera follow the character
	/// </summary>
	public class FollowCamera : MonoBehaviour
	{
		/// <summary>
		/// Triggers after the frame update
		/// </summary>
		void LateUpdate()
		{
			var character = CharacterController.Instance;
			if (null != character)
			{
				var characterPosition = character.transform.position;
				transform.position = new Vector3(characterPosition.x, characterPosition.y, transform.position.z);
			}
		}
	}
}
