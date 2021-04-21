using System;
using System.Collections.Generic;

namespace ClassLibrary.Figure.Generic
{
	public class EnumerableFugureIncrease<T> : ICollectionFigure<T> where T : AbstractFigure
	{
		private IEnumerable<T> _figureEnumerable;
		private IFigureIncrease<T> _figureIncrease = new IncreaseFigureParameters<T>();

		public EnumerableFugureIncrease(List<T> collestions)
		{
			_figureEnumerable = collestions ?? throw new ArgumentNullException(nameof(collestions));
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

			return _figureEnumerable;
		}
	}
}
