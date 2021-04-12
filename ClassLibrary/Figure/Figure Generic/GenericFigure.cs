using System;

namespace ClassLibrary.Figure.Generic
{
	interface IFigureIncrease<in T>
	{
		public void IncreasePointPosition(T obj, double coefficient);
	}

	public class IncreaseFigureParameters<T> : IFigureIncrease<T> where T : AbstractFigure
	{
		public void IncreasePointPosition(T obj, double coefficient)
		{
			if (obj == null)
				throw new ArgumentNullException($"Объект {nameof(obj)} является {obj}");

			obj.IncreasePointPosition(coefficient);
		}
	}
}
