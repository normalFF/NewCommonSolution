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
		Dictionary<Person, string> personDictionary = new Dictionary<Person, string>();
		Dictionary<Person, string> constantPersonDictionary = new Dictionary<Person, string>();
		List<Person> personList = new List<Person>();
		List<Person> constantPersonList = new List<Person>();

		Random rn = new Random();
		delegate void GenerateDictionaty(ref Dictionary<Person, string> keyValuePairs, IGetHashCode getHashCode, int val);
		delegate void GenerateList(ref List<Person> people, IGetHashCode getHashCode, int val);

		[Test]
		public void RunDictionaryPerson()
		{
			GenerateDictionaty generate = CreatePerson;
			CheckTimeGenerateDictionary(100, generate, ref personDictionary, new ImplementationBaseGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateDictionary(10000, generate, ref personDictionary, new ImplementationBaseGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateDictionary(20000, generate, ref personDictionary, new ImplementationBaseGetHashCode());
		}

		[Test]
		public void RunDictionaryConstantPerson()
		{
			GenerateDictionaty generate = CreatePerson;
			CheckTimeGenerateDictionary(100, generate, ref constantPersonDictionary, new ImplementationConstGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateDictionary(10000, generate, ref constantPersonDictionary, new ImplementationConstGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateDictionary(20000, generate, ref constantPersonDictionary, new ImplementationConstGetHashCode());
		}

		[Test]
		public void RunListPerson()
		{
			GenerateList generate = CreatePerson;
			CheckTimeGenerateList(100, generate, ref personList, new ImplementationBaseGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateList(10000, generate, ref personList, new ImplementationBaseGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateList(20000, generate, ref personList, new ImplementationBaseGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateList(50000, generate, ref personList, new ImplementationBaseGetHashCode());
		}

		[Test]
		public void RunConstantListPerson()
		{
			GenerateList generate = CreatePerson;
			CheckTimeGenerateList(100, generate, ref constantPersonList, new ImplementationConstGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateList(10000, generate, ref constantPersonList, new ImplementationConstGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateList(20000, generate, ref constantPersonList, new ImplementationConstGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateList(50000, generate, ref constantPersonList, new ImplementationConstGetHashCode());
		}

		private void CheckTimeGenerateList(int val, GenerateList func, ref List<Person> keyValuePairs, IGetHashCode getHashCode)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			func(ref keyValuePairs, getHashCode, val);
			stopwatch.Stop();
			Console.WriteLine($"Время генерации для {val} объектов: {stopwatch.ElapsedTicks}");
		}

		private void CheckTimeGenerateDictionary(int val, GenerateDictionaty func, ref Dictionary<Person, string> keyValuePairs , IGetHashCode getHashCode)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			func(ref keyValuePairs, getHashCode, val);
			stopwatch.Stop();
			Console.WriteLine($"Время генерации для {val} объектов: {stopwatch.ElapsedTicks}");
		}

		private void CreatePerson(ref Dictionary<Person, string> keyValuePairs, IGetHashCode getHashCode, int val)
		{
			for (int i = 0; i < val; i++)
			{
				int RandomValue = rn.Next(1, 29);
				Person person = new Person(new NameSurname(Convert.ToString((Enums.Names)RandomValue), Enums.CorrectSurname(RandomValue, rn.Next(1, 33)), Enums.CorrectPatronymic(RandomValue, rn.Next(1, 19))),
					new DateTime(rn.Next(1960, 2001), rn.Next(1, 12), rn.Next(1, 28)), Convert.ToString((Enums.City)rn.Next(1, 7)), rn.Next(1000000, 10000000), getHashCode);

				keyValuePairs.Add(person, Enums.Workplace[rn.Next(0, 10)]);
			}
		}

		private void CreatePerson(ref List<Person> keyValuePairs, IGetHashCode getHashCode, int val)
		{
			for (int i = 0; i < val; i++)
			{
				int RandomValue = rn.Next(1, 29);
				Person person = new Person(new NameSurname(Convert.ToString((Enums.Names)RandomValue), Enums.CorrectSurname(RandomValue, rn.Next(1, 33)), Enums.CorrectPatronymic(RandomValue, rn.Next(1, 19))),
					new DateTime(rn.Next(1960, 2001), rn.Next(1, 12), rn.Next(1, 28)), Convert.ToString((Enums.City)rn.Next(1, 7)), rn.Next(1000000, 10000000), getHashCode);

				keyValuePairs.Add(person);
			}
		}
	}
}
