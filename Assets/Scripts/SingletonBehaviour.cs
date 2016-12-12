using UnityEngine;

/// <summary>
/// Singleton behavior class
/// </summary>
public class SingletonBehaviour<T> : MonoBehaviour where T : Component
{
	/// <summary>
	/// Holds the actual instance
	/// </summary>
	static T _instance;

	/// <summary>
	/// Accessor for the instance of the singleton
	/// </summary>
	public static T Instance
	{
		get
		{
			// Attempt to find an existing instance if necessary
			if (null == _instance)
			{
				_instance = FindObjectOfType(typeof(T)) as T;
			}

			// If the instance is still null, create a new one
			if (null == _instance)
			{
				var go = new GameObject();
				// Hide the object so this script can manage it directly
				go.hideFlags = HideFlags.HideAndDontSave;
				_instance = go.AddComponent<T>();
			}

			return _instance;
		}
	}

	/// <summary>
	/// Initializer that deletes the object if an instance already exists
	/// </summary>
	void Awake()
	{
		if (null != _instance)
		{
			Destroy(_instance);
		}
	}
}