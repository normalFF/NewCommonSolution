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
		Random rn = new Random();
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
			Dictionary<Human, string> persons = new Dictionary<Human, string>();
			Faker faker = new Faker("ru");

			for (int i = 0; i < count; i++)
			{
				var gender = faker.Person.Gender;
				Human person = new Human(faker.Name.FirstName(gender) + faker.Name.FullName(gender), new DateTime(1990, 9, 9), faker.Address.City(), 19999999, getHashCode);
			}
		}

		private void CreatePersonList(IGetHashCode getHashCode, int count)
		{
			List<Person> persons = new List<Person>();

			for (int i = 0; i < count; i++)
			{
				Person person = new Person();
				persons.Add(person);
			}
		}
	}
}
