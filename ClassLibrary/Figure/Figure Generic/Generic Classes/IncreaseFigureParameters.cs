using System;

namespace ClassLibrary.Figure.Generic
{
	public class IncreaseFigureParameters<T> : IFigureIncrease<T> where T : AbstractFigure
	{
		delegate void SetMessage(string nameObject, double coefficient);
		event SetMessage IncreaseFigureEvent;

		public IncreaseFigureParameters()
		{
			IncreaseFigureEvent += PrintMessageToConsole;
		}

		public void IncreasePointPosition(T obj, double coefficient)
		{
			if (obj == null)
				throw new ArgumentNullException($"Объект {nameof(obj)} является null");

			obj.IncreasePointPosition(coefficient);
			IncreaseFigureEvent?.Invoke(nameof(obj), coefficient);
		}

		private void PrintMessageToConsole(string nameObject, double coefficient)
		{
			Console.WriteLine($"Значения координат точек {nameObject} были увеличены в {coefficient} раз(-а)");
		}
	}
}
