using System;
using NUnit.Framework;
using ClassLibrary.OtherObjects;
using Bogus;

namespace Tests
{
	[TestFixture]
	class TestDataBase
	{
		HumanDataBase dataBase = new HumanDataBase();

		[Test]
		public void AddHumanAndCreateFile()
		{
			Faker faker = new Faker();
			for (int i = 0; i < 10; i++)
			{
				Human human = new Human(faker.Name.FullName(), new DateTime(faker.Random.Int(1980, 2000), faker.Random.Int(1, 12), faker.Random.Int(1, 28)),
					faker.Address.Country(), faker.Random.Int(1000000, 9999999), new ImplementationBaseGetHashCode());
				dataBase.AddHuman(human);
			}

			dataBase.Save();
		}
	}
}
