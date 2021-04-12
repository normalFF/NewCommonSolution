using System;
using System.IO;
using System.Collections.Generic;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase
{
	public partial class HumanDataBase
	{
		private string _pathFile = Directory.GetCurrentDirectory() + @"\";
		private string _fileName = "DataBase.txt";
		private bool _isLoaded = false;

		private Dictionary<int, Human> _dataBase = new Dictionary<int, Human>();

		public HumanDataBase(Human human)
		{
			AddHuman(human);
		}

		public HumanDataBase()
		{

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
				SetMessageEvent(FileNotFound, _pathFile + _fileName);
				return;
			}

			Dictionary<int, Human> loadDataBase = new Dictionary<int, Human>();

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

					SetMessageEvent(FileWasOpened, _pathFile + _fileName);
				}
				else
				{
					SetMessageEvent(FileNotDataBase, _pathFile + _fileName);
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
		}

		public void Save()
		{
			if (!_isLoaded && File.Exists(_pathFile + _fileName))
			{
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

			SetMessageEvent(FileSave, _pathFile + _fileName);
			CreateOrUpdateCatalog(_pathFile);
		}

		public void AddHuman(Human human)
		{
			if (human == null)
				throw new ArgumentNullException(nameof(human));

			if (_dataBase.ContainsValue(human))
			{
				SetMessageEvent(HumanIsContained, human.GetHashCode());
				return;
			}

			_dataBase.Add(human.GetHashCode(), human);
			SetMessageEvent(HumanAdded, human.GetHashCode());
		}

		public void RemoveHuman(Human human)
		{
			if (human == null || !(_dataBase.ContainsValue(human)))
				return;

			_dataBase.Remove(human.GetHashCode());
			SetMessageEvent(HumanRemoved, human.GetHashCode());
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
				SetMessageEvent(CreateHTMLCatalog, _filePath + @"CatalogHTML\");
			}
			else
			{
				var item = Directory.GetFiles(_filePath + @"CatalogHTML\");

				if (item.Length == 0)
				{
					CreateDirectoryHTML(_filePath + @"CatalogHTML\");
					SetMessageEvent(CreateHTMLCatalog, _filePath + @"CatalogHTML\");
				}
				else
				{
					foreach (var file in item)
					{
						File.Delete(file);
					}
					CreateDirectoryHTML(_filePath + @"CatalogHTML\");
					SetMessageEvent(UpdateHTMLCatalog, _filePath + @"CatalogHTML\");
				}
			}
		}

		private static Human ConvertStringToHuman(string human)
		{
			if (human == null)
				throw new ArgumentNullException(nameof(human) + $" является null");

			string[] infoHuman = human.Split(" ");

			if (infoHuman.Length == 7)
				return new Human($"{infoHuman[0]} {infoHuman[1]}", 
									Convert.ToDateTime(infoHuman[2] + " " + infoHuman[3]), 
									infoHuman[4] + " " + infoHuman[5], 
									Convert.ToInt32(infoHuman[infoHuman.Length - 1]), 
									new ImplementationBaseGetHashCode());

			return new Human($"{infoHuman[0]} {infoHuman[1]}", 
								Convert.ToDateTime(infoHuman[2] + " " + infoHuman[3]), 
								infoHuman[4], 
								Convert.ToInt32(infoHuman[infoHuman.Length - 1]), 
								new ImplementationBaseGetHashCode());
		}

		private static string ConvertHumanToString(Human human)
		{
			if (human == null)
				throw new ArgumentNullException(nameof(human));

			return human.NameSurnamePatronymic.ToString() + $" {human.DateBirth} {human.PlaceBirth} {human.Passport}";
		}
	}
}
