using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.HierarchyBaseObjects
{
	public class Person : People
	{
		public bool IsVaccinated
		{
			get; protected set;
		}
		public DateTime DateofBirth
		{
			get; protected set;
		}

		public Person(double mass, string name, int age, DateTime date, bool vaccinated) : base(mass, name, age)
		{
			IsVaccinated = vaccinated;
			DateofBirth = date;
		}

		public override string ToString()
		{
			return base.ToString() + $", Дата рождения: {DateofBirth}, Вакцинирован: {IsVaccinated}";
		}
	}
}
