using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.OtherObjects
{
	partial class HumanDataBase
	{
		delegate void MessageObject(int hashCode);
		delegate void MessageFile(string filePath);
		event MessageFile ThrowEventOperationFile;
		event MessageObject ThrowEventOperationObject;

		private void SetMessageEvent(MessageObject message, int hashCode)
		{
			ThrowEventOperationObject += message;
			ThrowEventOperationObject.Invoke(hashCode);
			ThrowEventOperationObject -= message;
		}

		private void SetMessageEvent(MessageFile message, string filePath)
		{
			ThrowEventOperationFile += message;
			ThrowEventOperationFile.Invoke(filePath);
			ThrowEventOperationFile -= message;
		}

		private static void HumanAdded(int hashCode)
		{
			Console.WriteLine($"Персона была добавлена в базу данных. HashCode: {hashCode}");
		}

		private static void HumanRemoved(int hashCode)
		{
			Console.WriteLine($"Персона была удалена из базы данных. HashCode: {hashCode}");
		}

		private static void HumanIsContained(int hashCode)
		{
			Console.WriteLine($"Персона уже содержится в базе данных. HashCode: {hashCode}");
		}

		private static void FileRewrite(string pathFile)
		{
			Console.WriteLine($"Файл {pathFile} был обновлён");
		}

		private static void FileSave(string pathFile)
		{
			Console.WriteLine($"Файл {pathFile} был сохранён");
		}

		private static void FileWasOpened(string pathFile)
		{
			Console.WriteLine($"Файл {pathFile} был успешно открыт");
		}

		private static void FileNotFound(string pathFile)
		{
			Console.WriteLine($"Файл {pathFile} не найден");
		}

		private static void FileNotDataBase(string pathFile)
		{
			Console.WriteLine($"Файл {pathFile} не является базой данных");
		}

		private static void CreateHTMLCatalog(string pathFile)
		{
			Console.WriteLine($"Каталог {pathFile} создан");
		}

		private static void UpdateHTMLCatalog(string pathFile)
		{
			Console.WriteLine($"Каталог {pathFile} обновлён");
		}
	}
}
