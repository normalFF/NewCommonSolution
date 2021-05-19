using System;
using System.Collections.Generic;
using ClassLibrary.OtherObjects;

namespace ClassLibrary.DataBase.DataSerialization
{
	internal interface ISerializationData
	{
		List<Human> Load();

		void Save(List<Human> humen);
	}
}
