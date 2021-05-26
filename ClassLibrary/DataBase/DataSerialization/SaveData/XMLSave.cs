using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase.DataSerialization
{
	class XMLSave : IConcreteSerializationSave
	{
		private event Action<string, string> ThrowEventOperationFile;
		void Message(string info, string filePath)
		{
			Console.WriteLine($"Файл {filePath}: {info}");
		}

		public XMLSave()
		{
			ThrowEventOperationFile += Message;
		}

		public void Save(List<Human> humen, string filePath)
		{
			string message;

			if (!File.Exists(filePath))
			{
				message = "Файл будет создан";
			}
			else
			{
				message = "Файл будет перезаписан";
			}

			ThrowEventOperationFile?.Invoke(message, filePath);

			try
			{
				using (FileStream file = new(filePath, FileMode.OpenOrCreate, FileAccess.Write))
				{
					XmlSerializer xml = new(typeof(List<Human>));
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
				ThrowEventOperationFile?.Invoke(message, filePath);
			}
		}
	}
}
