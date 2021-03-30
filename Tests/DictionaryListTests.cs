using System;
using System.Diagnostics;
using System.Collections.Generic;
using NUnit.Framework;
using ClassLibrary.OtherObjects;

namespace Tests
{
	[TestFixture]
	class DictionaryListTests
	{
		Random rn = new Random();
		delegate void GenerateDictionaty(IGetHashCode getHashCode, int val);
		delegate void GenerateList(IGetHashCode getHashCode, int val);

		[Test]
		public void RunDictionaryPerson()
		{
			GenerateDictionaty generate = CreatePersonDictionary;
			CheckTimeGenerateDictionary(100, generate, new ImplementationBaseGetHashCode());
			CheckTimeGenerateDictionary(10000, generate, new ImplementationBaseGetHashCode());
			CheckTimeGenerateDictionary(20000, generate, new ImplementationBaseGetHashCode());
		}

		[Test]
		public void RunDictionaryPersonConstant()
		{
			GenerateDictionaty generate = CreatePersonDictionary;
			CheckTimeGenerateDictionary(100, generate, new ImplementationConstGetHashCode());
			CheckTimeGenerateDictionary(10000, generate, new ImplementationConstGetHashCode());
			CheckTimeGenerateDictionary(20000, generate, new ImplementationConstGetHashCode());
		}

		[Test]
		public void RunListPerson()
		{
			GenerateList generate = CreatePersonList;
			CheckTimeGenerateList(100, generate, new ImplementationBaseGetHashCode());
			CheckTimeGenerateList(10000, generate, new ImplementationBaseGetHashCode());
			CheckTimeGenerateList(20000, generate, new ImplementationBaseGetHashCode());
			CheckTimeGenerateList(50000, generate, new ImplementationBaseGetHashCode());
		}

		[Test]
		public void RunListPersonConstant()
		{
			GenerateList generate = CreatePersonList;
			CheckTimeGenerateList(100, generate, new ImplementationConstGetHashCode());
			CheckTimeGenerateList(10000, generate, new ImplementationConstGetHashCode());
			CheckTimeGenerateList(20000, generate, new ImplementationConstGetHashCode());
			CheckTimeGenerateList(50000, generate, new ImplementationConstGetHashCode());
		}

		private void CheckTimeGenerateList(int count, GenerateList func, IGetHashCode getHashCode)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			func(getHashCode, count);
			stopwatch.Stop();
			Console.WriteLine($"Время генерации для {count} объектов: {stopwatch.ElapsedMilliseconds}");
		}

		private void CheckTimeGenerateDictionary(int count, GenerateDictionaty func, IGetHashCode getHashCode)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			func(getHashCode, count);
			stopwatch.Stop();
			Console.WriteLine($"Время генерации для {count} объектов: {stopwatch.ElapsedMilliseconds}");
		}

		private void CreatePersonDictionary(IGetHashCode getHashCode, int count)
		{
			Dictionary<Person, string> persons = new Dictionary<Person, string>();

			for (int i = 0; i < count; i++)
			{
				int randomValue = rn.Next(1, 29);
				Person person = new Person(new NameSurname(Convert.ToString((Enums.Names)randomValue), Enums.CorrectSurname(randomValue, rn.Next(1, 33)), Enums.CorrectPatronymic(randomValue, rn.Next(1, 19))),
					new DateTime(rn.Next(1960, 2001), rn.Next(1, 12), rn.Next(1, 28)), Convert.ToString((Enums.City)rn.Next(1, 7)), rn.Next(1000000, 10000000), getHashCode);

				persons.Add(person, Enums.Workplace[rn.Next(0, 10)]);
			}
		}

		private void CreatePersonList(IGetHashCode getHashCode, int count)
		{
			List<Person> persons = new List<Person>();

			for (int i = 0; i < count; i++)
			{
				int randomValue = rn.Next(1, 29);
				Person person = new Person(new NameSurname(Convert.ToString((Enums.Names)randomValue), Enums.CorrectSurname(randomValue, rn.Next(1, 33)), Enums.CorrectPatronymic(randomValue, rn.Next(1, 19))),
					new DateTime(rn.Next(1960, 2001), rn.Next(1, 12), rn.Next(1, 28)), Convert.ToString((Enums.City)rn.Next(1, 7)), rn.Next(1000000, 10000000), getHashCode);

				persons.Add(person);
			}
		}
	}
}
