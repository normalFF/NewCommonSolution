using System;

namespace ClassLibrary.OtherObjects
{
	public struct NameSurname
	{
		public string Name { get; private set; }
		public string Surname { get; private set; }
		public string Patronymic { get; private set; }

		public NameSurname(string name, string surname, string patronymic)
		{
			Name = name ?? throw new ArgumentNullException("Присваивание null в NameSurname.Name");
			Surname = surname ?? throw new ArgumentNullException("Присваивание null в NameSurname.Surname");
			Patronymic = patronymic ?? throw new ArgumentNullException("Присваивание null в NameSurname.Patronymic");
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is NameSurname))
				return false;

			NameSurname nsp = (NameSurname)obj;
			return Equals(Name, nsp.Name) && Equals(Surname, nsp.Surname) && Equals(Patronymic, nsp.Patronymic);
		}

		public override string ToString()
		{
			return $"{Name} {Surname} {Patronymic}";
		}
	}

	public class Person
	{
		public NameSurname NameSurnamePatronymic { get; protected set; }
		public DateTime DateBirth { get; protected set; }
		public int Passport { get; protected set; }
		public string PlaceBirth { get; protected set; }

		private IGetHashCode _getCode;

		public Person(NameSurname name, DateTime date, string place, int passport, IGetHashCode getHashCode)
		{
			if (name.Name == null || name.Patronymic == null || name.Surname == null)
				throw new ArgumentNullException("Имя, фамилия и отчество не может быть null");

			if (passport < 1000000 && passport > 9999999)
				throw new ArgumentOutOfRangeException("Недопустимый номер паспорта");

			PlaceBirth = place ?? throw new ArgumentNullException("Место рождения не может быть null");

			NameSurnamePatronymic = name;
			DateBirth = date;
			Passport = passport;
			_getCode = getHashCode;
		}

		public override int GetHashCode()
		{
			return _getCode.SetParameters(Passport, NameSurnamePatronymic.Name.Length, NameSurnamePatronymic.Patronymic.Length, NameSurnamePatronymic.Surname.Length);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Person))
				return false;

			Person p = obj as Person;
			return NameSurnamePatronymic.Equals(p.NameSurnamePatronymic) && DateBirth == p.DateBirth && Equals(PlaceBirth, p.PlaceBirth);
		}

		public static bool operator ==(Person personOne, Person personTwo) => personOne.Equals(personTwo);

		public static bool operator !=(Person personOne, Person personTwo) => personOne.Equals(personTwo);

		public override string ToString()
		{
			return NameSurnamePatronymic.ToString() + $"\nДата рождения: {DateBirth}\nМесто рождения: {PlaceBirth}";
		}
	}

	public interface IGetHashCode
	{
		public int SetParameters(params int[] array);
	}

	public class ImplementationConstGetHashCode : IGetHashCode
	{
		int IGetHashCode.SetParameters(params int[] array)
		{
			return 7;
		}
	}

	public class ImplementationBaseGetHashCode : IGetHashCode
	{
		int IGetHashCode.SetParameters(params int[] array)
		{
			return (array[0] << 2) + array[1] * array[2] * array[3];
		}
	}
}
