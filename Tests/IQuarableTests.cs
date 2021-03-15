using NUnit.Framework;
using System;
using ClassLibrary.OtherObjects;
using System.Linq;

namespace Tests
{
	class IQuarableTests
	{
		Person[] _collectivePerson;

		private void Generate()
		{
			Random rn = new Random();
			_collectivePerson = new Person[100];

			for (int i = 0; i < _collectivePerson.Length; i++)
			{
				int RandomValue = rn.Next(1, 29);
				_collectivePerson[i] = new Person(new NameSurname(Convert.ToString((Enums.Names)RandomValue), Enums.CorrectSurname(RandomValue, rn.Next(1, 33)), Enums.CorrectPatronymic(RandomValue, rn.Next(1, 19))), 
					new DateTime(rn.Next(1980, 2001), rn.Next(1, 12), rn.Next(1, 28)), Convert.ToString((Enums.City)rn.Next(1, 7)), rn.Next(1000000, 10000000));
			}
		}

		[SetUp]
		public void SetUp()
		{
			Generate();
		}

		[Test]
		public void SamplePersonByName()
		{
		}

		[Test]
		public void SamplePersonByAge()
		{
		}

		[Test]
		public void SamplePersonByBool()
		{
		}

		[Test]
		public void SamplePersonByDateofBirth()
		{
		}
	}
}