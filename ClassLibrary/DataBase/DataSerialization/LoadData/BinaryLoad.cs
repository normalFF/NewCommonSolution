using System;
using System.IO;
using System.Security;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase.DataSerialization
{
	internal class BinaryLoad : IConcreteSerializationLoad
	{
		private event Action<string, string> ThrowEventOperationFile;
		void Message(string info, string filePath)
		{
			Console.WriteLine($"Файл {filePath}: {info}");
		}

		public BinaryLoad()
		{
			ThrowEventOperationFile += Message;
		}

		public List<Human> Load(string filePath)
		{
			string message = null;

			try
			{
				FileInfo fileInfo = new(filePath);
				if (!fileInfo.Extension.Equals(".dat", StringComparison.OrdinalIgnoreCase))
				{
					throw new FileLoadException($"Недопустимый формат файла: {fileInfo.Extension}, ожидался файл расширения .dat");
				}

				ThrowEventOperationFile?.Invoke("Попытка получения данных", filePath);

				if (File.Exists(filePath))
				{
					List<Human> collection;

					using (FileStream file = new(filePath, FileMode.Open, FileAccess.Read))
					{
						BinaryFormatter binary = new();
						collection = (List<Human>)binary.Deserialize(file);
					}

					message = "Данные получены успешно";
					return collection;
				}
				else
				{
					message = "Файл с расширением .dat не найден";
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
