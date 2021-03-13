using System;

namespace ClassLibrary.HierarchyBaseObjects
{
	public partial class People : ICloneable
	{
		public object Clone()
		{
			return new People(this.Mass, this.Name, this.Age);
		}
	}
}
