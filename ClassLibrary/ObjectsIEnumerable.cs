using System;
using System.Collections;

namespace ClassLibrary
{
	public class ObjectsEnumerable<T> : IEnumerable
		where T : class
	{
		private T[] _objects;

		public ObjectsEnumerable(T[] objects)
		{
			_objects = objects;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator GetEnumerator()
		{
			return new ObjectsEnumerator<T>(_objects);
		}

		private class ObjectsEnumerator<U> : IEnumerator
		{
			private U[] _objects;
			int _position = -1;
			object IEnumerator.Current
			{
				get
				{
					return Current;
				}
			}
			U Current
			{
				get
				{
					try
					{
						return _objects[_position];
					}
					catch (IndexOutOfRangeException)
					{
						throw new InvalidCastException();
					}
				}
			}

			public ObjectsEnumerator(U[] objects)
			{
				_objects = objects;
			}

			public bool MoveNext()
			{
				_position++;
				return (_position > -1 && _position < _objects.Length);
			}

			public void Reset()
			{
				_position = -1;
			}
		}
	}
}
