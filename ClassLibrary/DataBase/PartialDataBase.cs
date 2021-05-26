using ClassLibrary.DataBase.DataSerialization;
using ClassLibrary.OtherObjects;
using System;
using System.Collections.Generic;
using System.IO;

namespace ClassLibrary.DataBase
{
	partial class HumanDataBase
	{
		event Action<string, int> EventOperationObject;
		event Action<string, string> EventOperationDataBase;

		private void PrintObjectMessageToConsole(string objectOperation, int hashCode)
		{
			Console.WriteLine(objectOperation + " HashCode: " + hashCode);
		}

		private void PrintDataBaseMessageToConsole(string dataBaseOperation, string nameDataBase)
		{
			Console.WriteLine($"{dataBaseOperation}: {nameDataBase}");
		}

		private class DataBase
		{
			private EnumDataSerializationLoad _dataLoad;
			private EnumDataSerializationSave _dataSave;
			private readonly string _filePath;
			private readonly string _currentFileExtension;

			private IConcreteSerializationLoad _concreteLoad;
			private IConcreteSerializationSave _concreteSave;

			public DataBase(string filePath, EnumDataSerializationLoad dataLoad, EnumDataSerializationSave dataSave)
			{
				_dataLoad = dataLoad;
				_dataSave = dataSave;
				_filePath = filePath;

				FileInfo file = new(_filePath);
				_currentFileExtension = file.Extension;

				SetConcreteSerialization(dataLoad, dataSave);
			}

			public List<Human> Load()
			{
				return _concreteLoad.Load(_filePath) ?? new();
			}

			public void Save(List<Human> humen)
			{
				_concreteSave.Save(humen, _filePath.Replace(_currentFileExtension, "." + _dataSave.ToString()));
			}

			public void SetConcreteSerialization(EnumDataSerializationLoad dataLoad, EnumDataSerializationSave dataSave)
			{
				switch (dataLoad)
				{
					case EnumDataSerializationLoad.xml:
						if (CheckSerialization(dataLoad))
						{
							_concreteLoad = new XMLLoad();
							_dataLoad = dataLoad;
						}
						break;

					case EnumDataSerializationLoad.json:
						if (CheckSerialization(dataLoad))
						{
							_concreteLoad = new JsonLoad();
							_dataLoad = dataLoad;
						}
						break;

					case EnumDataSerializationLoad.dat:
						if (CheckSerialization(dataLoad))
						{
							_concreteLoad = new BinaryLoad();
							_dataLoad = dataLoad;
						}
						break;
				}

				switch (dataSave)
				{
					case EnumDataSerializationSave.xml:
						if (CheckSerialization(dataSave))
						{
							_concreteSave = new XMLSave();
							_dataSave = dataSave;
						}
						break;

					case EnumDataSerializationSave.json:
						if (CheckSerialization(dataSave))
						{
							_concreteSave = new JsonSave();
							_dataSave = dataSave;
						}
						break;

					case EnumDataSerializationSave.dat:
						if (CheckSerialization(dataSave))
						{
							_concreteSave = new BinarySave();
							_dataSave = dataSave;
						}
						break;
				}
			}

			private bool CheckSerialization(EnumDataSerializationLoad dataLoad)
			{
				return _dataLoad.CompareTo(dataLoad) != 0 || _concreteLoad == null;
			}

			private bool CheckSerialization(EnumDataSerializationSave dataSave)
			{
				return _dataSave.CompareTo(dataSave) != 0 || _concreteSave == null;
			}
		}
	}
}
