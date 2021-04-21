using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase
{
	public partial class HumanDataBase
	{
		private string _pathFile = Directory.GetCurrentDirectory() + @"\";
		private string _fileName = "DataBase.txt";
		private bool _isLoaded = false;

		private Dictionary<int, Human> _dataBase = new();

		public HumanDataBase(Human human)
		{
			EventOperationFile += PrintFileMessageToConsole;
			EventOperationObject += PrintObjectMessageToConsole;
			AddHuman(human);
		}

		public HumanDataBase()
		{
			EventOperationFile += PrintFileMessageToConsole;
			EventOperationObject += PrintObjectMessageToConsole;
		}

		public void SetNameDataBase(string name)
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));

			if (!_fileName.Equals(name))
				_isLoaded = false;

			_fileName = name;
		}

		public void SetPathDataBase(string path)
		{
			if (path == null)
				throw new ArgumentNullException(nameof(path));

			if (!_pathFile.Equals(path))
				_isLoaded = false;

			_pathFile = path;
		}

		public void Load()
		{
			if (!File.Exists(_pathFile + _fileName))
			{
				EventOperationFile?.Invoke("Указанный файл не найден", _pathFile + _fileName);
				return;
			}

			Dictionary<int, Human> loadDataBase = new();

			using (var fileRead = new StreamReader(_pathFile + _fileName, System.Text.Encoding.UTF8))
			{
				string line = fileRead.ReadLine();
				if (line.Equals("БД"))
				{
					line = fileRead.ReadLine();
					while (line != null)
					{
						Human human = ConvertStringToHuman(line);
						loadDataBase.Add(human.GetHashCode(), human);
						line = fileRead.ReadLine();
					}

					EventOperationFile?.Invoke("Файл был открыт", _pathFile + _fileName);
				}
				else
				{
					EventOperationFile?.Invoke("Указанный файл не является базой данных", _pathFile + _fileName);
				}
			}

			if (_dataBase != null)
			{
				foreach (var item in _dataBase)
				{
					if (!loadDataBase.ContainsValue(item.Value))
						loadDataBase.Add(item.Key, item.Value);
				}
			}

			_isLoaded = true;
			_dataBase = loadDataBase;
			EventOperationFile?.Invoke("База данных ", _pathFile + _fileName);
		}

		public void Save()
		{
			if (!_isLoaded && File.Exists(_pathFile + _fileName))
			{
				EventOperationFile?.Invoke("Получение данных из файла", _pathFile + _fileName);
				Load();
			}

			using (var file = new StreamWriter(_pathFile + _fileName))
			{
				file.WriteLine("БД");
				foreach (var item in _dataBase)
				{
					file.WriteLine(ConvertHumanToString(item.Value));
				}
				file.Flush();
			}

			EventOperationFile?.Invoke("Данные записаны на файл", _pathFile + _fileName);
			CreateOrUpdateCatalog(_pathFile);
		}

		public void AddHuman(Human human)
		{
			if (human == null)
				throw new ArgumentNullException(nameof(human));

			if (_dataBase.ContainsValue(human))
			{
				EventOperationObject?.Invoke("Персона уже записана в БД", human.GetHashCode());
				return;
			}

			_dataBase.Add(human.GetHashCode(), human);
			EventOperationObject?.Invoke("Персона записана в БД", human.GetHashCode());
		}

		public void RemoveHuman(Human human)
		{
			if (human == null || !(_dataBase.ContainsValue(human)))
				return;

			_dataBase.Remove(human.GetHashCode());
			EventOperationObject?.Invoke("Персона удалена из БД", human.GetHashCode());
		}

		private void CreateOrUpdateCatalog(string _filePath)
		{
			void CreateDirectoryHTML(string _fullFilePath)
			{
				foreach (var human in _dataBase)
				{
					human.Value.AddHumanHTMLPage(_fullFilePath);
				}
			}

			if (!Directory.Exists(_filePath + "CatalogHTML"))
			{
				Directory.CreateDirectory(_filePath + "CatalogHTML");
				CreateDirectoryHTML(_filePath + @"CatalogHTML\");
				EventOperationFile?.Invoke("Каталог создан", _filePath + @"CatalogHTML\");
			}
			else
			{
				var item = Directory.GetFiles(_filePath + @"CatalogHTML\");

				if (item.Length == 0)
				{
					CreateDirectoryHTML(_filePath + @"CatalogHTML\");
					EventOperationFile?.Invoke("Каталог создан", _filePath + @"CatalogHTML\");
				}
				else
				{
					foreach (var file in item)
					{
						File.Delete(file);
					}
					CreateDirectoryHTML(_filePath + @"CatalogHTML\");
					EventOperationFile?.Invoke("Каталог обновлён", _filePath + @"CatalogHTML\");
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
