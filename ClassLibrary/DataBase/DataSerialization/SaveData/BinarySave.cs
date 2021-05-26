using System;
using System.IO;
using System.Security;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase.DataSerialization
{
	internal class BinarySave : IConcreteSerializationSave
	{
		private event Action<string, string> ThrowEventOperationFile;
		void Message(string info, string filePath)
		{
			Console.WriteLine($"Файл {filePath}: {info}");
		}

		public BinarySave()
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
				using (FileStream file = new(filePath, FileMode.OpenOrCreate))
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
				ThrowEventOperationFile?.Invoke(message, filePath);
			}
		}
	}
}
