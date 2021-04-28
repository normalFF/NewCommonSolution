using System;
using System.Collections.Generic;
using System.Reflection;

namespace ClassLibrary.OtherObjects
{
	public partial class MyDelegate
	{
		private List<MethodInfo> _methods;
		private Dictionary<int, Exception> _exceptions;

		internal SignatureFunction Signature { get; private set; }

		public MyDelegate(MethodInfo methodInfo)
		{
			if (methodInfo == null)
				throw new ArgumentNullException(nameof(methodInfo));

			_methods.Add(methodInfo);
			Signature = new SignatureFunction(methodInfo);
		}

		#region
		public object Invoke(object obj, object[] parameters)
		{
			_exceptions.Clear();

			object result = null;
			foreach (var item in _methods)
			{
				try
				{
					result = item.Invoke(obj, parameters);
				}
				catch (Exception ex)
				{
					if (ex.InnerException != null)
						_exceptions.Add(item.GetHashCode(), ex.InnerException);
				}
			}

			return result;
		}

		public void Add(MethodInfo methodInfo)
		{
			if (methodInfo == null)
				throw new ArgumentNullException(nameof(methodInfo));

			if (!Signature.CheckMethodInfo(methodInfo))
				throw new InvalidOperationException("Сигнатура метода не совпадает с сигнатурой делегата");

			_methods.Add(methodInfo);
		}

		public void Remove(MethodInfo methodInfo)
		{
			if (methodInfo == null)
				throw new ArgumentNullException(nameof(methodInfo));

			if (!Signature.CheckMethodInfo(methodInfo))
				throw new InvalidOperationException("Сигнатура метода не совпадает с сигнатурой делегата");

			_methods.Remove(methodInfo);
		}

		public void Clear()
		{
			_methods.Clear();
		}
		#endregion

		public override bool Equals(object obj)
		{
			if (obj == null)
				throw new ArgumentNullException(nameof(obj));

			if (!(obj is MyDelegate))
				throw new ArgumentException(nameof(obj));

			MyDelegate myDelegate = (MyDelegate)obj;

			return myDelegate.Signature.Equals(Signature);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
