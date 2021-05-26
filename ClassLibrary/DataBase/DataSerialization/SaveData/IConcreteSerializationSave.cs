using System.Collections.Generic;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase.DataSerialization
{
	interface IConcreteSerializationSave
	{
		void Save(List<Human> humen, string filePath);
	}
}
