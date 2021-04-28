using System;
using System.Linq;
using System.Reflection;

namespace ClassLibrary.OtherObjects
{
	partial class MyDelegate
	{
		internal class SignatureFunction
		{
			public Type ReturnType { get; private set; }
			public ParameterInfo[] Parameters { get; private set; }

			public SignatureFunction(MethodInfo methodInfo)
			{
				ReturnType = methodInfo.ReturnType;
				Parameters = methodInfo.GetParameters();
			}

			public bool CheckMethodInfo(MethodInfo methodInfo)
			{
				if (methodInfo == null)
					throw new ArgumentNullException(nameof(methodInfo));

				return Parameters.SequenceEqual(methodInfo.GetParameters()) && ReturnType.Equals(methodInfo.ReturnType);
			}

			public override bool Equals(object obj)
			{
				if (obj == null)
					throw new ArgumentNullException(nameof(obj));

				if (!(obj is SignatureFunction))
					throw new ArgumentNullException(nameof(obj));

				var signature = (SignatureFunction)obj;

				return Parameters.SequenceEqual(signature.Parameters) && signature.ReturnType.Equals(ReturnType);
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}
		}

		public static MyDelegate operator +(MyDelegate myDelegate, MethodInfo methodInfo)
		{
			myDelegate.Add(methodInfo);
			return myDelegate;
		}

		public static MyDelegate operator -(MyDelegate myDelegate, MethodInfo methodInfo)
		{
			myDelegate.Remove(methodInfo);
			return myDelegate;
		}
	}
}
