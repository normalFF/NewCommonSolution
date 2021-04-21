using System;
using System.Collections.Generic;

namespace ClassLibrary.Figure.Generic
{
	public class CollectionFigure<T> : ICollectionFigure<T> where T : AbstractFigure
	{
		delegate void SetMessage(string message);
		event SetMessage CollectionFigureEvent;
		private IEnumerable<T> _figureEnumerable;

		public CollectionFigure(List<T> collestions)
		{
			_figureEnumerable = collestions ?? throw new ArgumentNullException(nameof(collestions));
			CollectionFigureEvent += PrintMessageToConsole;
		}

		public IEnumerable<T> IncreasePointFigureToCollection(double coefficient)
		{
			if (coefficient == 0)
			{
				throw new ArgumentOutOfRangeException(nameof(coefficient));
			}

			foreach (var item in _figureEnumerable)
			{
				item.IncreasePointPosition(coefficient);
			}

			CollectionFigureEvent?.Invoke("Элементы коллекции изменены");
			return _figureEnumerable;
		}

		private void PrintMessageToConsole(string message)
		{
			Console.WriteLine(message);
		}
	}
}
