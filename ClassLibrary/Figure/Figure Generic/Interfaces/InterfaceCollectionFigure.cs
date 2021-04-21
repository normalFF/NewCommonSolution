using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Figure.Generic
{
	interface ICollectionFigure<out T> where T : AbstractFigure
	{
		IEnumerable<T> IncreasePointFigureToCollection(double coefficient);
	}
}
