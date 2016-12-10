using System.Collections.Generic;
using UnityEngine;

namespace Sparrow
{
	/// <summary>
	/// An object pool that pools and reuses objects
	/// </summary>
	public abstract class ObjectPool<T> : MonoBehaviour where T : Poolable
	{
		/// <summary>
		/// The pool
		/// </summary>
		readonly List<T> _pool = new List<T>();

		/// <summary>
		/// Obtains an object from the pool or creates a new one
		/// </summary>
		/// <returns>An object that is pooled</returns>
		public T Get()
		{
			lock (_pool)
			{
				// Try to reuse an object
				foreach (var entry in _pool)
				{
					if (entry.Reusable)
					{
						entry.Reusable = false;
						return entry;
					}
				}

				// If none exist, let's create a new one
				var newEntry = Create();
				newEntry.Reusable = false;
				_pool.Add(newEntry);
				return newEntry;
			}
		}

		/// <summary>
		/// Creates a new object that will be added to the pool
		/// </summary>
		/// <returns>Creates a new object that will be added to the pool.</returns>
		protected abstract T Create();
	}
}