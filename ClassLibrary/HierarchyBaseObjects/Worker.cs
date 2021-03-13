using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.HierarchyBaseObjects
{
	public class Worker : People
	{
		public string Company
		{
			get; protected set;
		}

		public Worker(double mass, string name, int age, string company) : base(mass, name, age)
		{
			Company = company ?? throw new ArgumentNullException("Company не может быть null");
		}

		public override string ToString()
		{
			return base.ToString() + $" компания: {Company}";
		}
	}
}
