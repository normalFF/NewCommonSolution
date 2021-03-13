using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.HierarchyBaseObjects
{
	public class BaseObject
	{
		public double Mass
		{
			get; protected set;
		}

		public BaseObject(double mass)
		{
			if (mass <= 0)
				throw new ArgumentOutOfRangeException("Масса не может быть отрицательной!");
			Mass = mass;
		}
	}
}
