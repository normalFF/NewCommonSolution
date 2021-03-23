using System;

namespace ClassLibrary.Figure
{
	public class Circle : AbstractFigure
	{
		private Point _cender;
		private double _radius;
		public Circle(Point cender,  double radius)
		{
			_cender = cender;

			if (radius <= 0)
				throw new ArgumentOutOfRangeException("");
			_radius = radius;
		}

		public override double GetPerimetr()
		{
			return Math.PI * Math.Pow(_radius, 2);
		}

		public override double GetArea()
		{
			return Math.PI * _radius * 2;
		}

		public override string ToString()
		{
			return "Фигура: Окружность" + base.ToString();
		}
	}
}
