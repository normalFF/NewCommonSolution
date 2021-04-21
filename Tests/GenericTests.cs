using System;
using System.Collections.Generic;
using NUnit.Framework;
using ClassLibrary.Figure;
using ClassLibrary.Figure.Generic;

namespace Tests
{
	[TestFixture]
	class GenericTests
	{
		List<Triangle> triangles = new();
		List<Circle> circles = new();
		List<Square> squares = new();

		public void Setup()
		{
			Generate();
		}

		public void Generate()
		{
			ICreateFigure<AbstractFigure> createFigure = new CreateFigure<Triangle>();

			for (int i = 0; i < 10; i++)
			{
				Triangle triangle = (Triangle)createFigure.Create();
				triangles.Add(triangle);
			}

			createFigure = new CreateFigure<Circle>();
			for (int i = 0; i < 10; i++)
			{
				Circle circle = (Circle)createFigure.Create();
				circles.Add(circle);
			}

			createFigure = new CreateFigure<Square>();
			for (int i = 0; i < 10; i++)
			{
				Square square = (Square)createFigure.Create();
				squares.Add(square);
			}
		}

		[Test]
		public void TestKontrvariableClass()
		{
			IFigureIncrease<AbstractFigure> figureIncrease = new IncreaseFigureParameters<AbstractFigure>();

			foreach (var item in circles)
			{
				Console.WriteLine(item.ToString());
				figureIncrease.IncreasePointPosition(item, 2);
			}
			foreach (var item in triangles)
			{
				Console.WriteLine(item.ToString());
				figureIncrease.IncreasePointPosition(item, 2);
			}
			foreach (var item in squares)
			{
				Console.WriteLine(item.ToString());
				figureIncrease.IncreasePointPosition(item, 2);
			}
		}
	}
}
