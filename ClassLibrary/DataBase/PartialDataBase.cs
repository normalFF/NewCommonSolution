using System;

namespace ClassLibrary.DataBase
{
	partial class HumanDataBase
	{
		delegate void MessageObject(string objectOperation, int hashCode);
		delegate void MessageFile(string fileOperation, string filePath);
		event MessageFile EventOperationFile;
		event MessageObject EventOperationObject;

		private void PrintObjectMessageToConsole(string objectOperation, int hashCode)
		{
			Console.WriteLine(objectOperation + " HashCode: " + hashCode);
		}

		private void PrintFileMessageToConsole(string objectOperation, string fullFilePath)
		{
			Console.WriteLine(objectOperation + ": " + fullFilePath);
		}
	}
}
