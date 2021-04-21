using System;

namespace ClassLibrary.Figure.Generic
{
	public class CreateFigure<T> : ICreateFigure<T> where T : AbstractFigure
	{
		private readonly IConcreteFigure _concreteFigure;

		public CreateFigure()
		{
			if (typeof(T) == typeof(Triangle))
			{
				_concreteFigure = new CreateTriangle();
			}
			if (typeof(T) == typeof(Square))
			{
				_concreteFigure = new CreateSquare();
			}
			if (typeof(T) == typeof(Circle))
			{
				_concreteFigure = new CreateSquare();
			}
		}

		public T Create()
		{
			return (T)_concreteFigure.Create();
		}

		private class CreateTriangle : IConcreteFigure
		{
			private Random _random = new();

			public object Create()
			{
				Point[] points = new Point[3];
				points[0] = new Point(_random.Next(-30, 0), _random.Next(0, 30));
				points[1] = new Point(_random.Next(0, 31), _random.Next(0, 30));
				points[2] = new Point(_random.Next(0, 31), _random.Next(-30, 0));
				return new Triangle(points);
			}
		}

		private class CreateSquare : IConcreteFigure
		{
			private Random _random = new();

			public object Create()
			{
				Point[] points = new Point[4];
				points[0] = new Point(_random.Next(-30, 0), _random.Next(0, 30));
				points[1] = new Point(_random.Next(0, 31), _random.Next(0, 30));
				points[2] = new Point(_random.Next(0, 31), _random.Next(-30, 0));
				points[3] = new Point(_random.Next(-30, 0), _random.Next(-30, 0));
				return new Square(points);
			}
		}

		private class CreateCircle : IConcreteFigure
		{
			private Random _random = new();

			public object Create()
			{
				Point point = new(_random.Next(-30, 30), _random.Next(-30, 30));
				return new Circle(point, _random.Next(1, 20));
			}
		}
	}
}
