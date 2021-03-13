using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.HierarchyBaseObjects
{
	public partial class People : BaseObject
	{
		public string Name
		{
			get; protected set;
		}
		public int Age
		{
			get; protected set;
		}

		public People(double mass, string name, int age) : base(mass)
		{
			Name = name ?? throw new ArgumentNullException("Name не может быть null");

			if (age < 0)
				throw new ArgumentOutOfRangeException("Возраст не может быть отрицательным числом!");
			Age = age;
		}

		public override string ToString()
		{
			return $"Имя: {Name}, возраст: {Age}, вес: {Mass}";
		}
	}
}
