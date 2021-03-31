using System;
using System.IO;
using NUnit.Framework;
using ClassLibrary.OtherObjects;

namespace Tests
{
	[TestFixture]
	class TestsPersonDatabase
	{
		Person[] personCollective;

		private void GeneratePerson()
		{
			personCollective = new Person[20];
			Random random = new Random();

			for (int i = 0; i < personCollective.Length; i++)
			{
				var randomValue = random.Next(1, 29);
				personCollective[i] = new Person(new NameSurname(Convert.ToString((Enums.Names)randomValue), Enums.CorrectSurname(randomValue, random.Next(1, 33)), Enums.CorrectPatronymic(randomValue, random.Next(1, 19))),
					new DateTime(random.Next(1960, 2001), random.Next(1, 12), random.Next(1, 28)), Convert.ToString((Enums.City)random.Next(1, 7)), random.Next(1000000, 10000000), new ImplementationBaseGetHashCode());
			}
		}

		[SetUp]
		public void Setup()
		{
			GeneratePerson();
		}

		[Test]
		public void TestAddDatabase()
		{
			bool CheckedAddPerson;
			for (int i = 0; i < personCollective.Length; i++)
			{
				CheckedAddPerson = personCollective[i].AddPersonHTMLPage(@"C:\Users\fgvng\Desktop\PersonData\");
				if (CheckedAddPerson)
					Console.WriteLine("Объект записан");
				else
					Console.WriteLine("Объект уже существует");
			}
		}
	}
}
