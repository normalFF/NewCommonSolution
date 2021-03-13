using System;
using NUnit.Framework;
using ClassLibrary;
using ClassLibrary.HierarchyBaseObjects;

namespace Tests
{
	[TestFixture]
	public class IComparableTests
	{
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
		People[] _collectivePeople;
		ObjectsEnumerable<People> objectsEnumerable;

		private void Generate()
		{
			Random rn = new Random();
			_collectivePeople = new People[10];

			for (int i = 0; i < _collectivePeople.Length; i++)
			{
				_collectivePeople[i] = new People(rn.Next(55, 70), Convert.ToString((Names)rn.Next(1, 20)), rn.Next(23, 35));
			}

			objectsEnumerable = new ObjectsEnumerable<People>(_collectivePeople);
		}

		private void Print()
		{
			foreach (People p in objectsEnumerable)
			{
				Console.WriteLine(p.ToString());
			}
		}

		[SetUp]
		public void Setup()
		{
			Generate();
		}

		[Test]
		public void SortByAgeAscending()
		{
			Array.Sort(_collectivePeople, People.sortAgeAscending());
			Console.WriteLine("Сортировка по возрасту по возрастанию");
			Print();
		}

		[Test]
		public void SortByNameAscending()
		{
			Array.Sort(_collectivePeople);
			Console.WriteLine("Сортировка по имени");
			Print();
		}

		[Test]
		public void SortByMassAscending()
		{
			Array.Sort(_collectivePeople, People.sortMassAscending());
			Console.WriteLine("Сортировка по массе по возрастанию");
			Print();
		}
	}
}
