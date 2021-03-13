using System;

namespace ClassLibrary.Figure
{
	class Circle : AbstractFigure
	{
		public Circle(double radius)
		{
			if (radius <= 0)
				throw new ArgumentOutOfRangeException("");

			Perimetr = GetPerimetr(radius);
			Area = GetArea(radius);
		}

		private static double GetPerimetr(double radius)
		{
			return Math.PI * Math.Pow(radius, 2);
		}

		private static double GetArea(double radius)
		{
			return Math.PI * radius * 2;
		}

		public override string ToString()
		{
			return "Фигура: Окружность" + base.ToString();
		}
	}
}
