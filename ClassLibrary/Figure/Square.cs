using System;

namespace ClassLibrary.Figure
{
	class Square : AbstractFigure
	{
		public Square(params Point[] points)
		{
			if (points.Length != 4)
				throw new ArgumentOutOfRangeException("");

			Perimetr = GetPerimetr(points);
			Area = GetArea(points);
		}

		private static double GetPerimetr(Point[] points)
		{
			return SqrtCalculate(points[0], points[1]) + SqrtCalculate(points[1], points[2]) +
				SqrtCalculate(points[2], points[3]) + SqrtCalculate(points[3], points[0]);
		}

		private static double GetArea(Point[] points)
		{
			return Math.Sqrt(SqrtCalculate(points[0], points[1]) * SqrtCalculate(points[1], points[2]) *
				SqrtCalculate(points[2], points[3]) * SqrtCalculate(points[3], points[0]));
		}

		public override string ToString()
		{
			return "Фигура: Четырёхугольник" + base.ToString();
		}
	}
}
