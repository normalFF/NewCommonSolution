using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase.DataSerialization
{
	internal class JsonLoad : IConcreteSerializationLoad
	{
		private event Action<string, string> ThrowEventOperationFile;

		void Message(string info, string filePath)
		{
			Console.WriteLine($"Файл {filePath}: {info}");
		}

		public JsonLoad()
		{
			ThrowEventOperationFile += Message;
		}

		public List<Human> Load(string filePath)
		{
			string message = null;

			try
			{
				FileInfo fileInfo = new(filePath);
				if (!fileInfo.Extension.Equals(".json", StringComparison.OrdinalIgnoreCase))
				{
					throw new FileLoadException($"Недопустимый формат файла: {fileInfo.Extension}, ожидался файл расширения .json");
				}

				ThrowEventOperationFile?.Invoke("Попытка получения данных", filePath);

				if (File.Exists(filePath))
				{
					List<Human> collection = null;

					using (FileStream file = new(filePath, FileMode.Open, FileAccess.Read))
					{
						bool complete = false;
						while (complete != true)
						{
							var resultOperation = JsonSerializer.DeserializeAsync<List<Human>>(file);
							if (resultOperation.IsCompleted)
							{
								collection = resultOperation.Result;
								complete = true;
							}
						}


						file.Flush();
					}

					message = "Данные получены успешно";
					return collection;
				}
				else
				{
					message = "Файл с расширением .json не найден";
					return null;
				}
			}
			catch (JsonException ex)
			{
				message = ex.Message;
				return null;
			}
			catch (NotSupportedException ex)
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
