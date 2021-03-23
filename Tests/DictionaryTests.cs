using System;
using System.Diagnostics;
using System.Collections.Generic;
using NUnit.Framework;
using ClassLibrary.OtherObjects;

namespace Tests
{
	[TestFixture]
	class DictionaryTests
	{
		Dictionary<Person, string> personDictionary = new Dictionary<Person, string>();
		Dictionary<Person, string> constantPersonDictionary = new Dictionary<Person, string>();

		Random rn = new Random();
		delegate void Generate(ref Dictionary<Person, string> keyValuePairs, IGetHashCode getHashCode, int val);

		[Test]
		public void RunPerson()
		{
			Generate generate = CreatePerson;
			CheckTimeGenerateDictionary(100, generate, ref personDictionary, new ImplementationBaseGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateDictionary(10000, generate, ref personDictionary, new ImplementationBaseGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateDictionary(20000, generate, ref personDictionary, new ImplementationBaseGetHashCode());
		}

		[Test]
		public void RunConstantPerson()
		{
			Generate generate = CreatePerson;
			CheckTimeGenerateDictionary(100, generate, ref constantPersonDictionary, new ImplementationConstGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateDictionary(10000, generate, ref constantPersonDictionary, new ImplementationConstGetHashCode());
			personDictionary.Clear();
			CheckTimeGenerateDictionary(20000, generate, ref constantPersonDictionary, new ImplementationConstGetHashCode());
		}

		private void CheckTimeGenerateDictionary(int val, Generate func, ref Dictionary<Person, string> keyValuePairs , IGetHashCode getHashCode)
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
	}
}
