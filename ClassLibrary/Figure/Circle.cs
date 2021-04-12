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
				throw new ArgumentOutOfRangeException($"Радиус {nameof(radius)} не может быть {radius}");
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

		public override void IncreasePointPosition(double coefficient)
		{
			if (coefficient == 0)
				throw new FormatException($"Значение {nameof(coefficient)} является {coefficient}");

			_cender.x *= coefficient;
			_cender.y *= coefficient;
		}

		public override string ToString()
		{
			return "Фигура: Окружность" + base.ToString();
		}
	}
}
