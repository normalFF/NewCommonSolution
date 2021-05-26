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
		[Test]
		public void SaveDataXML()
		{
			HumanDataBase humanDataBase = new(@"C:\Users\fgvng\Desktop\Новая папка\DataBase1.xml", EnumDataSerializationLoad.json, EnumDataSerializationSave.json);

			humanDataBase.Load();

			Faker faker = new();
			for (int i = 0; i < 10; i++)
			{
				Human human = new(faker.Name.FullName(), new DateTime(faker.Random.Int(1980, 2000), faker.Random.Int(1, 12), faker.Random.Int(1, 28)),
					faker.Address.Country(), faker.Random.Int(1000000, 9999999), new ImplementationBaseGetHashCode());
				humanDataBase.AddHuman(human);
			}

			humanDataBase.Save();
		}

		[Test]
		public void LoadDataXML()
		{
			HumanDataBase humanDataBase = new(@"C:\Users\fgvng\Desktop\Новая папка\DataBase.xml", EnumDataSerializationLoad.json, EnumDataSerializationSave.xml);

			humanDataBase.Load();
			humanDataBase.SetConcreteSerialization(EnumDataSerializationLoad.xml, EnumDataSerializationSave.xml);
			humanDataBase.Load();

			var list = humanDataBase.GetList();

			foreach (var item in list)
			{
				Console.WriteLine(item.ToString() + " " + item.GetHashCode());
			}
		}


		[Test]
		public void SaveDataJSON()
		{
			HumanDataBase humanDataBase = new(@"C:\Users\fgvng\Desktop\Новая папка\DataBase2.json", EnumDataSerializationLoad.json, EnumDataSerializationSave.json);

			Faker faker = new();
			for (int i = 0; i < 10; i++)
			{
				Human human = new(faker.Name.FullName(), new DateTime(faker.Random.Int(1980, 2000), faker.Random.Int(1, 12), faker.Random.Int(1, 28)),
					faker.Address.Country(), faker.Random.Int(1000000, 9999999), new ImplementationBaseGetHashCode());
				humanDataBase.AddHuman(human);
			}

			humanDataBase.Save();
		}

		[Test]
		public void LoadDataJSON()
		{
			HumanDataBase humanDataBase = new(@"C:\Users\fgvng\Desktop\Новая папка\DataBase3.json", EnumDataSerializationLoad.json, EnumDataSerializationSave.json);

			humanDataBase.Load();
			var list = humanDataBase.GetList();

			foreach (var item in list)
			{
				Console.WriteLine(item.ToString());
			}
		}

		[Test]
		public void SaveDataBinary()
		{
			HumanDataBase humanDataBase = new(@"C:\Users\fgvng\Desktop\Новая папка\DataBas4.dat", EnumDataSerializationLoad.dat, EnumDataSerializationSave.dat);

			Faker faker = new();
			for (int i = 0; i < 10; i++)
			{
				Human human = new(faker.Name.FullName(), new DateTime(faker.Random.Int(1980, 2000), faker.Random.Int(1, 12), faker.Random.Int(1, 28)),
					faker.Address.Country(), faker.Random.Int(1000000, 9999999), new ImplementationBaseGetHashCode());
				humanDataBase.AddHuman(human);
			}

			humanDataBase.Save();
		}

		[Test]
		public void LoadDataBinary()
		{
			HumanDataBase humanDataBase = new(@"C:\Users\fgvng\Desktop\Новая папка\DataBase5.dat", EnumDataSerializationLoad.dat, EnumDataSerializationSave.dat);

			humanDataBase.Load();
			var list = humanDataBase.GetList();

			foreach (var item in list)
			{
				Console.WriteLine(item.ToString() + " " + item.GetHashCode());
			}
		}
	}
}
