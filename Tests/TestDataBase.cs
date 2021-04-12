using System;
using NUnit.Framework;
using ClassLibrary.OtherObjects;
using ClassLibrary.DataBase;
using Bogus;

namespace Tests
{
	[TestFixture]
	class TestDataBase
	{
		HumanDataBase dataBaseOne = new HumanDataBase();
		HumanDataBase dataBaseTwo = new HumanDataBase();
		HumanDataBase dataBaseThree = new HumanDataBase();

		[SetUp]
		public void Setup()
		{
			dataBaseOne.SetPathDataBase(@"C:\Users\fgvng\Desktop\PersonData\Data1\");
			dataBaseTwo.SetPathDataBase(@"C:\Users\fgvng\Desktop\PersonData\Data2\");
			dataBaseThree.SetPathDataBase(@"C:\Users\fgvng\Desktop\PersonData\Data3\");
		}

		[Test]
		public void AddHumanAndCreateFile()
		{
			Faker faker = new Faker();
			for (int i = 0; i < 10; i++)
			{
				Human human = new Human(faker.Name.FullName(), new DateTime(faker.Random.Int(1980, 2000), faker.Random.Int(1, 12), faker.Random.Int(1, 28)),
					faker.Address.Country(), faker.Random.Int(1000000, 9999999), new ImplementationBaseGetHashCode());
				dataBaseOne.AddHuman(human);
			}

			dataBaseOne.Save();
		}

		[Test]
		public void AddHumanInDataBase()
		{
			Faker faker = new Faker();
			for (int i = 0; i < 10; i++)
			{
				Human human = new Human(faker.Name.FullName(), new DateTime(faker.Random.Int(1980, 2000), faker.Random.Int(1, 12), faker.Random.Int(1, 28)),
					faker.Address.Country(), faker.Random.Int(1000000, 9999999), new ImplementationBaseGetHashCode());
				dataBaseTwo.AddHuman(human);
			}

			dataBaseTwo.Save();
		}
	}
}
