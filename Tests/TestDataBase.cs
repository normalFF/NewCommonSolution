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
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void AddHumanAndCreateFile()
		{
			HumanDataBase humanDataBase = new(@"C:\Users\fgvng\Desktop\Новая папка\DataBase.txt");
			
			Faker faker = new();
			for (int i = 0; i < 10; i++)
			{
				Human human = new(faker.Name.FullName(), new DateTime(faker.Random.Int(1980, 2000), faker.Random.Int(1, 12), faker.Random.Int(1, 28)),
					faker.Address.Country(), faker.Random.Int(1000000, 9999999), new ImplementationBaseGetHashCode());
				humanDataBase.AddHuman(human);
			}

			humanDataBase.Save();
		}
	}
}
