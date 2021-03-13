using System;

namespace ClassLibrary.Figure
{
	public struct Point
	{
		public double x;
		public double y;

		public Point(double x, double y)
		{
			this.x = x;
			this.y = y;
		}
	}

	public abstract partial class AbstractFigure
	{
		public double Perimetr
		{
			get; protected set;
		}
		public double Area
		{
			get; protected set;
		}

		public override string ToString()
		{
			return $"\nПериметр: {Perimetr}\nПлощадь: {Area}";
		}

		protected static double SqrtCalculate(in Point p1, in Point p2)
		{
			return Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
		}
	}
}
