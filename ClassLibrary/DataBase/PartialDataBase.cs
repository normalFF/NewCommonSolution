using System;

namespace ClassLibrary.DataBase
{
	partial class HumanDataBase
	{
		event Action<string, int> EventOperationObject;
		event Action<string, string> EventOperationDataBase;

		private void PrintObjectMessageToConsole(string objectOperation, int hashCode)
		{
			Console.WriteLine(objectOperation + " HashCode: " + hashCode);
		}

		private void PrintDataBaseMessageToConsole(string dataBaseOperation, string nameDataBase)
		{
			Console.WriteLine($"{dataBaseOperation}: {nameDataBase}");
		}
	}
}
