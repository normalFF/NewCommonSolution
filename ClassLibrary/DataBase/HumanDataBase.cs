using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase
{
	public partial class HumanDataBase
	{
		private string _pathFile;
		private List<Human> _dataBase;


		public HumanDataBase(string pathFile)
		{
			if (!CheckCorrectPath(pathFile))
			{
				throw new FormatException($"Путь {pathFile} является некорректным");
			}

			EventOperationFile += PrintFileMessageToConsole;
			EventOperationObject += PrintObjectMessageToConsole;

			_dataBase = new();
			_pathFile = pathFile;

			if (File.Exists(pathFile))
			{
				EventOperationFile?.Invoke($"Файл Найден", pathFile);
				Load();
			}
			else
			{
				EventOperationFile?.Invoke($"Файл будет создан", pathFile);
			}
		}

		private bool CheckCorrectPath(string path)
		{
			var incorrectChars = Path.GetInvalidPathChars();

			foreach (var item in incorrectChars)
			{
				if (path.Contains(item))
				{
					return false;
				}
			}

			var Items = path.Split(Path.DirectorySeparatorChar);
			incorrectChars = Path.GetInvalidFileNameChars();

			foreach (var item in incorrectChars)
			{
				if (Items[Items.Length - 1].Contains(item))
				{
					return false;
				}
			}

			return true;
		}

		public void Load()
		{
			string message = null;

			try
			{
				using (var fileRead = new StreamReader(_pathFile, System.Text.Encoding.UTF8))
				{
					if (!CheckFileInDatabase(fileRead))
					{
						message = "Файл не является базой данных";
						return;
					}

					var line = fileRead.ReadLine();
					while (line != null)
					{
						Human human = ConvertStringToHuman(line);
						_dataBase.Add(human);
						line = fileRead.ReadLine();
					}

					message = "Данные получены успешно";
				}
			}
			finally
			{
				EventOperationFile(message, _pathFile);
			}
		}

		public void Save()
		{
			string message = null;
			
			try
			{
				using (var file = new StreamWriter(_pathFile))
				{
					if (_dataBase == null)
					{
						message = "Данные не были записаны на файл";
						return;
					}

					file.WriteLine("БД");
					foreach (var item in _dataBase)
					{
						file.WriteLine(ConvertHumanToString(item));
					}
					file.Flush();

					message = "Данные записаны на файл";
				}

				CreateOrUpdateCatalog();
			}
			finally
			{
				EventOperationFile?.Invoke(message, _pathFile);
			}
		}

		private bool CheckFileInDatabase(StreamReader file)
		{
			return file.ReadLine().Equals("БД");
		}
		
		private string CreatePathFileCatalog()
		{
			var fragmentsPath = _pathFile.Split(Path.DirectorySeparatorChar);
			string pathFileCatalog = fragmentsPath[0];

			for (int i = 1; i < fragmentsPath.Length - 1; i++)
			{
				pathFileCatalog = Path.Combine(pathFileCatalog, fragmentsPath[i]);
			}

			return Path.Combine(pathFileCatalog, "HumanCatalog") + Path.DirectorySeparatorChar;
		}

		public void AddHuman(Human human)
		{
			if (human == null)
				throw new ArgumentNullException(nameof(human));

			if (_dataBase.Contains(human))
			{
				EventOperationObject?.Invoke("Персона уже записана в БД", human.GetHashCode());
				return;
			}

			_dataBase.Add(human);
			EventOperationObject?.Invoke("Персона записана в БД", human.GetHashCode());
		}

		public void RemoveHuman(Human human)
		{
			if (human == null || !(_dataBase.Contains(human)))
				return;

			_dataBase.Remove(human);
			EventOperationObject?.Invoke("Персона удалена из БД", human.GetHashCode());
		}

		private void CreateOrUpdateCatalog()
		{
			void CreateDirectoryHTML(string pathFolder)
			{
				foreach (var human in _dataBase)
				{
					human.AddHumanHTMLPage(pathFolder);
				}
			}

			if (_dataBase == null)
			{
				return;
			}

			string catalogPath = CreatePathFileCatalog();

			if (!Directory.Exists(catalogPath))
			{
				Directory.CreateDirectory(catalogPath);
				CreateDirectoryHTML(catalogPath);
				EventOperationFile?.Invoke("Каталог создан", catalogPath);
			}
			else
			{
				var item = Directory.GetFiles(catalogPath);

				if (item.Length == 0)
				{
					CreateDirectoryHTML(catalogPath);
					EventOperationFile?.Invoke("Каталог создан", catalogPath);
				}
				else
				{
					foreach (var file in item)
					{
						File.Delete(file);
					}
					CreateDirectoryHTML(catalogPath);
					EventOperationFile?.Invoke("Каталог обновлён", catalogPath);
				}
			}
		}

		private static Human ConvertStringToHuman(string human)
		{
			if (human == null)
				throw new ArgumentNullException(nameof(human));

			string[] infoHuman = human.Split(" ");

			string name = infoHuman[0] + " " + infoHuman[1];
			DateTime dateTime = Convert.ToDateTime(infoHuman[2] + " " + infoHuman[3]);
			int passport = Convert.ToInt32(infoHuman[infoHuman.Length - 1]);
			
			StringBuilder place = new();
			for (int i = 4; i < infoHuman.Length - 1; i++)
			{
				place.Append(infoHuman[i] + " ");
			}

			return new Human(name, dateTime, place.ToString().Trim(), passport, new ImplementationBaseGetHashCode());
		}

		private static string ConvertHumanToString(Human human)
		{
			if (human == null)
				throw new ArgumentNullException(nameof(human));

			return human.NameSurnamePatronymic.ToString() + $" {human.DateBirth} {human.PlaceBirth} {human.Passport}";
		}
	}
}
