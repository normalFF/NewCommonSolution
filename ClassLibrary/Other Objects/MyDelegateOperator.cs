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

				if (methodInfo.GetParameters().Length != Parameters.Length)
					return false;

				var parameters = methodInfo.GetParameters();

				for (int i = 0; i < parameters.Length; i++)
				{
					if (!parameters[i].ParameterType.Equals(Parameters[i].ParameterType))
						return false;
				}

				return true && methodInfo.ReturnType.Equals(ReturnType);
			}

			public override bool Equals(object obj)
			{
				if (obj == null)
					throw new ArgumentNullException(nameof(obj));

				if (!(obj is SignatureFunction))
					throw new ArgumentNullException(nameof(obj));

				var signature = (SignatureFunction)obj;

				if (signature.Parameters.Length != Parameters.Length)
					return false;

				var parameters = signature.Parameters;

				for (int i = 0; i < parameters.Length; i++)
				{
					if (!parameters[i].ParameterType.Equals(Parameters[i].ParameterType))
						return false;
				}

				return true && signature.ReturnType.Equals(ReturnType);
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
