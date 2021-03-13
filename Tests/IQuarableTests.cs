using NUnit.Framework;
using System;
using ClassLibrary.HierarchyBaseObjects;
using System.Linq;

namespace Tests
{
	class IQuarableTests
	{
		Person[] _collectivePerson;
		public enum Names
		{
			Белла = 1,
			Валентин,
			Вадим,
			Борис,
			Галина,
			Дина,
			Джек,
			Глеб,
			Арина,
			Алан,
			Жанна,
			Захар,
			Иван,
			Егор,
			Лариса,
			Майя,
			Злата,
			Наум,
			Павел
		}

		private void Generate()
		{
			Random rn = new Random();
			_collectivePerson = new Person[100];

			for (int i = 0; i < _collectivePerson.Length; i++)
			{
				_collectivePerson[i] = new Person(rn.Next(55, 70), Convert.ToString((Names)rn.Next(1, 20)), rn.Next(23, 35),
					new DateTime(rn.Next(1985, 2000), rn.Next(1, 12), rn.Next(1, 28)), rn.Next(0, 2) > 0);
			}
		}

		[SetUp]
		public void SetUp()
		{
			Generate();
		}

		[Test]
		public void SamplePersonByName()
		{
			var personSorted = _collectivePerson.Where(t => t.Name.Length == 4);
			foreach (Person p in personSorted)
			{
				Console.WriteLine(p.ToString());
			}
		}

		[Test]
		public void SamplePersonByAge()
		{
			var personSorted = _collectivePerson.Where(t => t.Age % 2 == 0 && t.Mass % 2 == 0);
			foreach (Person p in personSorted)
			{
				Console.WriteLine(p.ToString());
			}
		}

		[Test]
		public void SamplePersonByBool()
		{
			int val = _collectivePerson.Where(t => t.IsVaccinated).Count();
			var personSorted = _collectivePerson.Where(t => t.IsVaccinated).Sum(t => t.Mass);
			Console.WriteLine($"Количество вакцинированных людей: {val}.\nСредняя масса вакцинированных людей: {personSorted / val}");
		}

		[Test]
		public void SamplePersonByDateofBirth()
		{
			var personSorted = _collectivePerson.Where(t => t.DateofBirth.Year > 1995).OrderBy(t => t.DateofBirth);
			foreach (Person p in personSorted)
			{
				Console.WriteLine(p.ToString());
			}
		}
	}
}