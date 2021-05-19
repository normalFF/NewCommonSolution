using System;

namespace ClassLibrary.OtherObjects
{
	public interface IGetHashCode
	{
		bool ReturnCorrectHashCode();

		public int SetParameters(params int[] array);
	}

	[Serializable]
	public class ImplementationConstGetHashCode : IGetHashCode
	{
		public bool ReturnCorrectHashCode()
		{
			return false;
		}

		int IGetHashCode.SetParameters(params int[] array)
		{
			return 7;
		}
	}

	[Serializable]
	public class ImplementationBaseGetHashCode : IGetHashCode
	{
		public bool ReturnCorrectHashCode()
		{
			return true;
		}

		int IGetHashCode.SetParameters(params int[] array)
		{
			return (array[0] << 2) + array[1] * array[2] * array[3] + array[4];
		}
	}
}
