/// <summary>
/// Singleton class
/// </summary>
public class Singleton<T> where T : new()
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
			// Create a new instance if needed
			if (null == _instance)
			{
				_instance = new T();
			}

			return _instance;
		}
	}

	/// <summary>
	/// Protected constructor to prohibit external creation
	/// </summary>
	protected Singleton() { }
}