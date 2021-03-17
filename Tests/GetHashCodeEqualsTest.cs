using System;
using NUnit.Framework;
using ClassLibrary.OtherObjects;

namespace Tests
{
	[TestFixture]
	class GetHashCodeEqualsTest
	{
		Person[] _person;

		[SetUp]
		public void Setup()
		{
			Generate();
		}

		void Generate()
		{
			_person = new Person[3];

			NameSurname name = new NameSurname(Convert.ToString((Enums.Names)4), Enums.CorrectSurname(4, 9), Enums.CorrectPatronymic(4, 9));
			DateTime dateTime = new DateTime(1990, 12, 20);

			_person[0] = new Person(name, dateTime, Convert.ToString((Enums.City)2), 1111111);
			_person[1] = _person[0].Clone() as Person;

			name = new NameSurname(Convert.ToString((Enums.Names)2), Enums.CorrectSurname(2, 5), Enums.CorrectPatronymic(2, 12));
			dateTime = new DateTime(1991, 10, 7);
			_person[2] = new Person(name, dateTime, Convert.ToString((Enums.City)1), 2222222);
		}

		[Test]
		public void TestGetHashCode()
		{
			for (int i = 0; i < _person.Length; i++)
			{
				Console.WriteLine(_person[i].ToString());
				Console.WriteLine(_person[i].GetHashCode());
				Console.WriteLine();
			}
		}

		[Test]
		public void TestEquals()
		{
			Assert.IsTrue(_person[0].Equals(_person[1]));
			Assert.IsTrue(_person[0] == _person[1]);

			Assert.IsTrue(!(_person[0].Equals(_person[2])));
			Assert.IsTrue(!(_person[0] == _person[2]));

			Assert.IsTrue(!(_person[0] != _person[1]));
			Assert.IsTrue(_person[0] != _person[2]);
		}
	}
}
