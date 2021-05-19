using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using ClassLibrary.OtherObjects;
using System.Text;

namespace ClassLibrary.DataBase.DataSerialization
{
	internal class JSONData : ISerializationData
	{
		private event Action<string, string> ThrowEventOperationFile;
		private readonly string _fullPathFile;
		private readonly JsonSerializerOptions _jsonOptions;

		void Message(string info, string filePath)
		{
			Console.WriteLine($"Файл {filePath}: {info}");
		}

		public JSONData(string fullPathFile)
		{
			_fullPathFile = fullPathFile ?? throw new ArgumentNullException(nameof(fullPathFile));
			ThrowEventOperationFile += Message;

			_jsonOptions = new JsonSerializerOptions
			{
				IgnoreNullValues = true, WriteIndented = true
			};
		}

		public List<Human> Load()
		{
			string message = null;

			try
			{
				ThrowEventOperationFile?.Invoke("Попытка получения данных", _fullPathFile);

				if (File.Exists(_fullPathFile))
				{
					List<Human> collection = null;

					using (FileStream file = new(_fullPathFile, FileMode.Open, FileAccess.Read))
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
					message = "Файл не найден";
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
				using (FileStream file = new(_fullPathFile, FileMode.OpenOrCreate, FileAccess.Write))
				{
					string json = JsonSerializer.Serialize(humen, typeof(List<Human>), _jsonOptions);
					byte[] bytes = Encoding.UTF8.GetBytes(json);
					file.Write(bytes, 0, bytes.Length);
				}

				message = "Данные записаны на файл";
			}
			catch (ArgumentException ex)
			{
				message = ex.Message;
			}
			catch (NotSupportedException ex)
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
