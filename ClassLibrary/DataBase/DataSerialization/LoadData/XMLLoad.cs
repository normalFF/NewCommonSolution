using System;
using System.IO;
using System.Collections.Generic;
using ClassLibrary.OtherObjects;
using System.Xml.Serialization;

namespace ClassLibrary.DataBase.DataSerialization
{
	internal class XMLLoad : IConcreteSerializationLoad
	{
		private event Action<string, string> ThrowEventOperationFile;
		void Message(string info, string filePath)
		{
			Console.WriteLine($"Файл {filePath}: {info}");
		}

		public XMLLoad()
		{
			ThrowEventOperationFile += Message;
		}

		public List<Human> Load(string filePath)
		{
			string message = null;

			try
			{
				FileInfo fileInfo = new(filePath);
				if (!fileInfo.Extension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
				{
					throw new FileLoadException($"Недопустимый формат файла: {fileInfo.Extension}, ожидался файл расширения .xml");
				}

				ThrowEventOperationFile?.Invoke("Попытка получения данных", filePath);

				if (File.Exists(filePath))
				{
					List<Human> collection;

					using (FileStream file = new(filePath, FileMode.Open, FileAccess.Read))
					{
						XmlSerializer xml = new(typeof(List<Human>));
						collection = (List<Human>)xml.Deserialize(file);
					}

					message = "Данные получены успешно";
					return collection;
				}
				else
				{
					message = "Файл с расширением .xml не найден";
					return null;
				}
			}
			catch (InvalidOperationException ex)
			{
				message = ex.InnerException.Message;
				return null;
			}
			catch (FileLoadException ex)
			{
				message = ex.Message;
				return null;
			}
			finally
			{
				ThrowEventOperationFile?.Invoke(message, filePath);
			}
		}
	}
}
