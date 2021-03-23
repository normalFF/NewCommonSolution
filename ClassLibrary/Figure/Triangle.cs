using System;

namespace ClassLibrary.Figure
{
	public class Triangle : AbstractFigure
	{
		private Point[] _figurePoints;
		public Triangle(params Point[] points)
		{
			if (points.Length != 3)
				throw new ArgumentOutOfRangeException("");
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

		public override string ToString()
		{
			return "Фигура: Треугольник" + base.ToString();
		}
	}
}
