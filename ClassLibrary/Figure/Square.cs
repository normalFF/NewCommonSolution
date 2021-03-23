using System;

namespace ClassLibrary.Figure
{
	public class Square : AbstractFigure
	{
		private Point[] _figurePoints;

		public Square(params Point[] points)
		{
			if (points.Length != 4)
				throw new ArgumentOutOfRangeException("");
			_figurePoints = points;
		}

		public override double GetPerimetr()
		{
			return SqrtCalculate(_figurePoints[0], _figurePoints[1]) + SqrtCalculate(_figurePoints[1], _figurePoints[2]) +
				SqrtCalculate(_figurePoints[2], _figurePoints[3]) + SqrtCalculate(_figurePoints[3], _figurePoints[0]);
		}

		public override double GetArea()
		{
			return Math.Sqrt(SqrtCalculate(_figurePoints[0], _figurePoints[1]) * SqrtCalculate(_figurePoints[1], _figurePoints[2]) *
				SqrtCalculate(_figurePoints[2], _figurePoints[3]) * SqrtCalculate(_figurePoints[3], _figurePoints[0]));
		}

		public override string ToString()
		{
			return "Фигура: Четырёхугольник" + base.ToString();
		}
	}
}
