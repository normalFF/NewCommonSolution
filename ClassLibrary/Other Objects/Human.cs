using System;

namespace ClassLibrary.OtherObjects
{
	[Serializable]
	public struct NameSurname
	{
		public string Name { get; set; }
		public string Surname { get; set; }

		public NameSurname(string name, string surname)
		{
			Name = name ?? throw new ArgumentNullException($"Значение {nameof(name)} не может быть {name}");
			Surname = surname ?? throw new ArgumentNullException($"Значение {nameof(surname)} не может быть {surname}");
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is NameSurname))
				return false;

			NameSurname nsp = (NameSurname)obj;
			return Equals(Name, nsp.Name) && Equals(Surname, nsp.Surname);
		}

		public override string ToString()
		{
			return $"{Name} {Surname}";
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

	[Serializable]
	public class Human
	{
		public NameSurname NameSurnamePatronymic { get; set; }
		public DateTime DateBirth { get; set; }
		public int Passport { get; set; }
		public string PlaceBirth { get; set; }
		public bool GetSetMethodHashCode
		{
			get
			{
				return _getCode.ReturnCorrectHashCode();
			}
			set
			{
				if (value) _getCode = new ImplementationBaseGetHashCode();
				else _getCode = new ImplementationConstGetHashCode();
			}
		}

		private IGetHashCode _getCode;

		public Human(string fullName, DateTime date, string place, int passport, IGetHashCode getHashCode)
		{
			if (fullName == null)
				throw new ArgumentNullException($"Значение {nameof(fullName)} не может быть {fullName}");

			if (passport < 1000000 && passport > 9999999)
				throw new ArgumentOutOfRangeException($"Значение {nameof(passport)} не может быть {passport}");

			PlaceBirth = place ?? throw new ArgumentNullException($"Значение {nameof(place)} не может быть {place}");

			NameSurnamePatronymic = GetFullName(fullName);
			DateBirth = date;
			Passport = passport;
			_getCode = getHashCode;
		}

		public Human() { }

		private static NameSurname GetFullName(string fullName)
		{
			string[] nameSurname = fullName.Split(" ");
			if (nameSurname.Length != 2)
				throw new FormatException($"Значение {nameof(nameSurname)} не соответствует входным данным");

			return new NameSurname(nameSurname[0], nameSurname[1]);
		}

		public override int GetHashCode()
		{
			return _getCode.SetParameters(Passport, NameSurnamePatronymic.Name.Length, NameSurnamePatronymic.Surname.Length, PlaceBirth.Length, DateBirth.Year);
		}

		public override string ToString()
		{
			return NameSurnamePatronymic.ToString() + $"\nДата рождения: {DateBirth}\nМесто рождения: {PlaceBirth}";
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Human))
				return false;

			Human p = obj as Human;
			return NameSurnamePatronymic.Equals(p.NameSurnamePatronymic) && DateBirth == p.DateBirth && Equals(PlaceBirth, p.PlaceBirth);
		}

		public static bool operator ==(Human personOne, Human personTwo) => personOne.Equals(personTwo);

		public static bool operator !=(Human personOne, Human personTwo) => personOne.Equals(personTwo);
	}
}
