namespace Sparrow
{
	/// <summary>
	/// If an object can be pooled
	/// </summary>
	public abstract class Poolable
	{
		/// <summary>
		/// If the object can be reused or not
		/// </summary>
		/// <value><c>true</c> if reusable; otherwise, <c>false</c>.</value>
		public bool Reusable { get; set; }
	}
}
