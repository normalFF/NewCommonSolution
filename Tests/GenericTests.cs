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
		List<AbstractFigure> abstractFigures = new();
		List<Circle> circles = new();
		List<Square> squares = new();

		public void Setup()
		{
			Generate();
		}

		public void Generate()
		{
			GenerateFigure<Triangle> generateTriangle = new();
			GenerateFigure<Square> generateSquare = new();
			GenerateFigure<Circle> generateCircle = new();

			for (int i = 0; i < 10; i++)
			{
				Circle circle = generateCircle.Create();
				Triangle triangle = generateTriangle.Create();
				Square square = generateSquare.Create();

				squares.Add(square);
				circles.Add(circle);
				triangles.Add(triangle);
				abstractFigures.Add(circle);
				abstractFigures.Add(square);
				abstractFigures.Add(triangle);
			}
		}

		[Test]
		public void TestKontrvariableClass()
		{
			IncreaseFigureParameters<AbstractFigure> kontrvariableClass = new();

			foreach (var item in circles)
			{
				kontrvariableClass.IncreasePointPosition(item, 2);
			}
			foreach (var item in triangles)
			{
				kontrvariableClass.IncreasePointPosition(item, 2);
			}
			foreach (var item in abstractFigures)
			{
				kontrvariableClass.IncreasePointPosition(item, 2);
			}
			foreach (var item in squares)
			{
				kontrvariableClass.IncreasePointPosition(item, 2);
			}
		}
	}
}
