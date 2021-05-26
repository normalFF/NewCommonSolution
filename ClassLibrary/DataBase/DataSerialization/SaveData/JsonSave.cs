using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase.DataSerialization
{
	internal class JsonSave : IConcreteSerializationSave
	{
		private event Action<string, string> ThrowEventOperationFile;
		private readonly JsonSerializerOptions _jsonOptions;

		void Message(string info, string filePath)
		{
			Console.WriteLine($"Файл {filePath}: {info}");
		}

		public JsonSave()
		{
			ThrowEventOperationFile += Message;
			
			_jsonOptions = new()
			{
				WriteIndented = true,
				IgnoreNullValues = true
			};
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
				ThrowEventOperationFile?.Invoke(message, filePath);
			}
		}
	}
}
