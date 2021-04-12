using System;
using System.Diagnostics;
using System.Collections.Generic;
using Bogus;
using NUnit.Framework;
using ClassLibrary.OtherObjects;

namespace Tests
{
	[TestFixture]
	class DictionaryListTests
	{
		delegate void GenerateDictionaty(IGetHashCode getHashCode, int val);
		delegate void GenerateList(IGetHashCode getHashCode, int count);

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

		private static void CheckTimeGenerateList(int count, GenerateList func, IGetHashCode getHashCode)
		{
			Stopwatch stopwatch = new();
			stopwatch.Start();
			func(getHashCode, count);
			stopwatch.Stop();
			Console.WriteLine($"Время генерации для {count} объектов: {stopwatch.ElapsedMilliseconds}");
		}

		private static void CheckTimeGenerateDictionary(int count, GenerateDictionaty func, IGetHashCode getHashCode)
		{
			Stopwatch stopwatch = new();
			stopwatch.Start();
			func(getHashCode, count);
			stopwatch.Stop();
			Console.WriteLine($"Время генерации для {count} объектов: {stopwatch.ElapsedMilliseconds}");
		}

		private void CreatePersonDictionary(IGetHashCode getHashCode, int count)
		{
			Dictionary<Human, string> persons = new();
			Faker faker = new("ru");
			DateTime dateTime;

			for (int i = 0; i < count; i++)
			{
				dateTime = new(faker.Random.Int(1980, 2000), faker.Random.Int(1, 12), faker.Random.Int(1, 28));
				var gender = faker.Person.Gender;
				Human human = new(faker.Name.FirstName(gender) + faker.Name.FullName(gender), dateTime, faker.Address.City(), 19999999, getHashCode);
				persons.Add(human, faker.Address.City());
			}
		}

		private void CreatePersonList(IGetHashCode getHashCode, int count)
		{
			List<Human> persons = new();
			Faker faker = new();
			DateTime dateTime;

			for (int i = 0; i < count; i++)
			{
				dateTime = new(faker.Random.Int(1980, 2000), faker.Random.Int(1, 12), faker.Random.Int(1, 28));
				var gender = faker.Person.Gender;
				Human person = new(faker.Name.FirstName(gender) + faker.Name.FullName(gender), dateTime, faker.Address.City(), 19999999, getHashCode);
				persons.Add(person);
			}
		}
	}
}
