using System;

namespace ClassLibrary.Figure
{
	public class Triangle : AbstractFigure
	{
		private Point[] _figurePoints;
		public Triangle(params Point[] points)
		{
			if (points.Length != 3)
				throw new ArgumentOutOfRangeException($"Количество точек в {nameof(points)} не может быть {points.Length}");
			_figurePoints = points;
		}

		public override double GetPerimetr()
		{
			return SqrtCalculate(_figurePoints[0], _figurePoints[1]) + SqrtCalculate(_figurePoints[1], _figurePoints[2]) +
				SqrtCalculate(_figurePoints[2], _figurePoints[0]);
		}

		public override double GetArea()
		{
			return 0.5 * ((_figurePoints[0].x - _figurePoints[2].x) * (_figurePoints[1].y - _figurePoints[2].y) - 
				(_figurePoints[1].x - _figurePoints[2].x) * (_figurePoints[0].y - _figurePoints[2].y));
		}

		public override void IncreasePointPosition(double coefficient)
		{
			if (coefficient == 0)
				throw new FormatException($"Значение {nameof(coefficient)} является {coefficient}");

			for (int i = 0; i < _figurePoints.Length; i++)
			{
				_figurePoints[i].x *= coefficient;
				_figurePoints[i].y *= coefficient;
			}
		}

		public override string ToString()
		{
			return "Фигура: Треугольник" + base.ToString();
		}
	}
}
