using System;
using System.IO;
using System.Collections.Generic;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase
{
	public partial class HumanDataBase
	{
		private List<Human> _humanList;
		private readonly DataBase _dataBase;

		public HumanDataBase(string pathFile, EnumDataSerializationLoad serializationLoad, EnumDataSerializationSave serializationSave)
		{
			if (!CheckCorrectPath(pathFile))
			{
				throw new FormatException($"Путь {pathFile} является некорректным");
			}

			EventOperationDataBase += PrintDataBaseMessageToConsole;
			EventOperationObject += PrintObjectMessageToConsole;

			_humanList = new();
			_dataBase = new(pathFile, serializationLoad, serializationSave);

			EventOperationDataBase?.Invoke("объект взаимодействия с базой данных инициализирован", nameof(HumanDataBase));
		}

		public void SetConcreteSerialization(EnumDataSerializationLoad serializationLoad, EnumDataSerializationSave serializationSave)
		{
			_dataBase.SetConcreteSerialization(serializationLoad, serializationSave);
		}

		public void Save()
		{
			if (_humanList != null)
			{
				_dataBase.Save(_humanList);
			}
		}

		public void Load()
		{
			_humanList = _dataBase.Load();
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

			if (_humanList.Contains(human))
			{
				EventOperationObject?.Invoke("Персона уже записана в БД", human.GetHashCode());
				return;
			}

			_humanList.Add(human);
			EventOperationObject?.Invoke("Персона записана в БД", human.GetHashCode());
		}

		public void RemoveHuman(Human human)
		{
			if (human == null || !(_humanList.Contains(human)))
				return;

			_humanList.Remove(human);
			EventOperationObject?.Invoke("Персона удалена из БД", human.GetHashCode());
		}

		public List<Human> GetList()
		{
			return _humanList;
		}
	}
}
