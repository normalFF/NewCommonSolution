using System;

namespace ClassLibrary.Figure
{
	public class Triangle : AbstractFigure
	{
		public Triangle(params Point[] points)
		{
			if (points.Length != 3)
				throw new ArgumentOutOfRangeException("");

			Perimetr = GetPerimetr(points);
			Area = GetArea(points);
		}

		private static double GetPerimetr(Point[] points)
		{
			return SqrtCalculate(points[0], points[1]) + SqrtCalculate(points[1], points[2]) +
				+SqrtCalculate(points[2], points[0]);
		}

		private static double GetArea(Point[] points)
		{
			return 0.5 * ((points[0].x - points[2].x) * (points[1].y - points[2].y) - (points[2].x - points[3].x) * (points[0].y - points[2].y));
		}

		public override string ToString()
		{
			return "Фигура: Треугольник" + base.ToString();
		}
	}
}
