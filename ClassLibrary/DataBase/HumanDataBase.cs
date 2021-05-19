using System;
using System.IO;
using System.Collections.Generic;
using ClassLibrary.OtherObjects;
using ClassLibrary.DataBase.DataSerialization;

namespace ClassLibrary.DataBase
{
	public partial class HumanDataBase
	{
		private List<Human> _dataBase;
		private ISerializationData _serialization;

		public HumanDataBase(string pathFile)
		{
			if (!CheckCorrectPath(pathFile))
			{
				throw new FormatException($"Путь {pathFile} является некорректным");
			}

			SetFormatSerialization(pathFile);

			EventOperationDataBase += PrintDataBaseMessageToConsole;
			EventOperationObject += PrintObjectMessageToConsole;

			_dataBase = new();

			EventOperationDataBase?.Invoke("База данных инициализирована", nameof(HumanDataBase));
		}

		public void Save()
		{
			if (_dataBase != null)
			{
				_serialization.Save(_dataBase);
			}
		}

		public void Load()
		{
			_dataBase = _serialization.Load();
		}

		private void SetFormatSerialization(string fileName)
		{
			FileInfo file = new(fileName);
			var fileExtension = file.Extension;

			if (fileExtension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
			{
				_serialization = new XMLData(fileName);
				return;
			}
			else if (fileExtension.Equals(".json", StringComparison.OrdinalIgnoreCase))
			{
				_serialization = new JSONData(fileName);
				return;
			}
			else if (fileExtension.Equals(".dat", StringComparison.OrdinalIgnoreCase))
			{
				_serialization = new BinaryData(fileName);
			}
			else
			{
				throw new FormatException("Неподдерживаемый формат файла");
			}
		}

		private static bool CheckCorrectPath(string path)
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

		public List<Human> GetList()
		{
			return _dataBase;
		}
	}
}
