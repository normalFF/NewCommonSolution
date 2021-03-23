using System;

namespace ClassLibrary.OtherObjects
{
	public static class Enums
	{
		public enum City
		{
			Тирасполь = 1,
			Бендеры,
			Слободзея,
			Григориополь,
			Каменка,
			Дуббосары
		};

		public enum Names 
		{
			Белла = 1,
			Галина,
			Дина,
			Арина,
			Жанна,
			Лариса,
			Майя,
			Злата,
			Зинаида,
			Дария,
			Ирина,
			Людмила,
			Надежда,
			Олеся = 14,
			Валентин,
			Вадим,
			Борис,
			Глеб,
			Алан,
			Захар,
			Иван,
			Егор,
			Наум,
			Павел,
			Игнат,
			Олег,
			Леонид,
			Харитон,
		};

		public enum Surname 
		{
			Алабаев = 1,
			Акустьев,
			Акимов,
			Алиев,
			Бабиков,
			Бабахин,
			Бабенин,
			Бабкин,
			Вавалина,
			Ваганков,
			Валеев,
			Валерьев,
			Гавренев,
			Гаврилов,
			Гавришов,
			Гаганов = 16,
			Абраменко,
			Авраменко,
			Авсеенко,
			Адаменко,
			Бабарыко,
			Бабаченко,
			Бабенко,
			Барано,
			Вага,
			Вакуленко,
			Варченко,
			Васечко,
			Галенко,
			Гальченко,
			Гапоненко,
			Глушенко
		};

		public enum Patronymic 
		{
			Абрамо,
			Августо,
			Агапо,
			Антропо,
			Артёмо,
			Аскольдо,
			Богдано,
			Вадимо,
			Валериано,
			Варламо,
			Венедикто,
			Василье,
			Виссарионо,
			Владиславо,
			Гелие,
			Генрихо,
			Герасимо,
			Германо,
			Гурие
		};

		public static string[] Workplace = new string[]
		{
			"Зеленый рынок",
			"Супермаркет Шериф",
			"ККК Тирасполь",
			"Завод Молдавизолит",
			"Дом культуры",
			"Школа",
			"Индивидуальный предприниматель",
			"Завод Квинт",
			"Продуктовый магазин",
			"Строительная компания",
		};

		public static string CorrectSurname(int indexName, int indexSurname)
		{
			if (indexName < 15 && indexSurname <= 16)
				return Convert.ToString((Surname)indexSurname) + "а";

			return Convert.ToString((Surname)indexSurname);
		}

		public static string CorrectPatronymic(int indexName, int indexPatronymic)
		{
			if (indexName < 15)
				return Convert.ToString((Patronymic)indexPatronymic) + "вна";
			else
				return Convert.ToString((Patronymic)indexPatronymic) + "вич";
		}
	}
}
