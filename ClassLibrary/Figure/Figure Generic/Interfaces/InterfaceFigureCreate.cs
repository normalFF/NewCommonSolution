using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Figure.Generic
{
	interface IConcreteFigure
	{
		public object Create();
	}

	public interface ICreateFigure<out T>
	{
		public T Create();
	}
}
