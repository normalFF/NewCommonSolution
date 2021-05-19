using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using ClassLibrary.OtherObjects;
using System.Runtime.Serialization;
using System.Security;

namespace ClassLibrary.DataBase.DataSerialization
{
	class BinaryData : ISerializationData
	{
		private event Action<string, string> ThrowEventOperationFile;
		private readonly string _fullPathFile;

		void Message(string info, string filePath)
		{
			Console.WriteLine($"Файл {filePath}: {info}");
		}

		public BinaryData(string fullPathFile)
		{
			_fullPathFile = fullPathFile ?? throw new ArgumentNullException(nameof(fullPathFile));
			ThrowEventOperationFile += Message;
		}

		public List<Human> Load()
		{
			string message = null;

			try
			{
				ThrowEventOperationFile?.Invoke("Попытка получения данных", _fullPathFile);

				if (File.Exists(_fullPathFile))
				{
					List<Human> collection;

					using (FileStream file = new(_fullPathFile, FileMode.Open, FileAccess.Read))
					{
						BinaryFormatter binary = new();
						collection = (List<Human>)binary.Deserialize(file);
					}

					message = "Данные получены успешно";
					return collection;
				}
				else
				{
					message = "Файл не найден";
					return null;
				}
			}
			catch (SerializationException ex)
			{
				message = ex.Message;
				return null;
			}
			catch (SecurityException ex)
			{
				message = ex.Message;
				return null;
			}
			finally
			{
				ThrowEventOperationFile?.Invoke(message, _fullPathFile);
			}
		}

		public void Save(List<Human> humen)
		{
			string message;

			if (!File.Exists(_fullPathFile))
			{
				message = "Файл будет создан";
			}
			else
			{
				message = "Файл будет перезаписан";
			}

			ThrowEventOperationFile?.Invoke(message, _fullPathFile);

			try
			{
				using (FileStream file = new(_fullPathFile, FileMode.OpenOrCreate))
				{
					BinaryFormatter binary = new();
					binary.Serialize(file, humen);
					file.Flush();
				}

				message = "Данные записаны на файл";
			}
			catch (SerializationException ex)
			{
				message = ex.Message;
			}
			catch (SecurityException ex)
			{
				message = ex.Message;
			}
			finally
			{
				ThrowEventOperationFile?.Invoke(message, _fullPathFile);
			}
		}
	}
}
