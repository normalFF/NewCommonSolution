using System.Collections.Generic;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase.DataSerialization
{
	interface IConcreteSerializationLoad
	{
		List<Human> Load(string filePath);
	}
}
