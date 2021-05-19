using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase.DataSerialization
{
	internal class XMLData : ISerializationData
	{
		private event Action<string, string> ThrowEventOperationFile;
		private readonly string _fullPathFile;

		void Message(string info, string filePath)
		{
			Console.WriteLine($"Файл {filePath}: {info}");
		}

		public XMLData(string fullPathFile)
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
						XmlSerializer xml = new(typeof(List<Human>));
						collection = (List<Human>)xml.Deserialize(file);
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
			catch (InvalidOperationException ex)
			{
				message = ex.InnerException.Message;
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
				XmlSerializer xml = new(typeof(List<Human>));

				using (FileStream file = new(_fullPathFile, FileMode.OpenOrCreate, FileAccess.Write))
				{
					xml.Serialize(file, humen);
					file.Flush();
				}

				message = "Данные записаны на файл";
			}
			catch (InvalidOperationException ex)
			{
				message = ex.InnerException.Message;
			}
			finally
			{
				ThrowEventOperationFile?.Invoke(message, _fullPathFile);
			}
		}
	}
}
