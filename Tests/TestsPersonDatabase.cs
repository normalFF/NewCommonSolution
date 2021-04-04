using System;
using Bogus;
using NUnit.Framework;
using ClassLibrary.OtherObjects;

namespace Tests
{
	[TestFixture]
	class TestsPersonDatabase
	{
		Human[] personCollective;

		private void GeneratePerson()
		{
			personCollective = new Human[20];
			Faker faker = new Faker("ru");

			for (int i = 0; i < personCollective.Length; i++)
			{
				personCollective[i] = new Human(faker.Name.FullName(), new DateTime(1990, 9, 9), faker.Address.City(), 19999999, new ImplementationBaseGetHashCode());
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
