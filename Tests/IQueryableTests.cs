using System;
using NUnit.Framework;
using System.Linq;
using ClassLibrary.HierarchyBaseObjects;
using ClassLibrary.OtherObjects;

namespace Tests
{
	[TestFixture]
	class IQueryableTests
	{
		People[] CollectivePeople;

		[SetUp]
		public void Setup()
		{
			Generate();
		}

		void Generate()
		{
			Random random = new Random();
			CollectivePeople = new People[100];

			for (int i = 0; i < CollectivePeople.Length; i++)
			{
				CollectivePeople[i] = new People(random.Next(55, 75), Convert.ToString((Enums.Names)random.Next(1, 29)), random.Next(20, 41));
			}
		}

		[Test]
		public void TestWhereLinq()
		{
			Console.WriteLine("Отбор по массе и возрасту");
			var result = CollectivePeople.Where(t => t.Mass > 64 && t.Age > 30);
			foreach (People p in result)
			{
				Console.WriteLine(p.ToString());
			}
		}

		[Test]
		public void TestOrderBy()
		{
			Console.WriteLine("Сортировка по длине имени");
			var result = CollectivePeople.OrderBy(t => t.Name.Length);
			foreach (People p in result)
			{
				Console.WriteLine(p.ToString());
			}
		}

		[Test]
		public void TestGroupBy()
		{
			Console.WriteLine("Группировка по возрасту\n");
			var result = CollectivePeople.GroupBy(t => t.Age).OrderBy(t => t.Key);
			foreach (IGrouping<int, People> p in result)
			{
				Console.WriteLine(p.Key);
				foreach (var i in p)
				{
					Console.WriteLine(i.ToString());
				}
			}
		}

		[Test]
		public void TestAnyAll()
		{
			bool result = CollectivePeople.All(t => t.Age > 26);
			Console.WriteLine($"Соответствие объектов условию Age > 26: {result}");
			result = CollectivePeople.Any(t => t.Age == 40 && t.Mass == 74);
			Console.WriteLine($"Соответствие одного из объектов условию Age = 40 и Mass = 74: {result}");
		}

		[Test]
		public void TestMinMaxSum()
		{
			double result = CollectivePeople.Sum(t => t.Mass) / CollectivePeople.Length;
			Console.WriteLine($"Среднее значение массы: {result}");
			result = CollectivePeople.Min(t => t.Mass);
			Console.WriteLine($"Минимальное значение массы: {result}");
			result = CollectivePeople.Max(t => t.Mass);
			Console.WriteLine($"Максимальное значение массы: {result}");
		}
	}
}
