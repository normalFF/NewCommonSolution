using NUnit.Framework;
using System;
using ClassLibrary;
using ClassLibrary.Figure;

namespace Tests
{
	class TestSortFigure
	{
		Random rn = new();
		AbstractFigure[] arrayFigure;
		ObjectsEnumerable<AbstractFigure> enumerableFigure;

		private void Generate()
		{
			arrayFigure = new AbstractFigure[50];

			for (int i = 0; i < arrayFigure.Length; i++)
			{
				int timeValue = rn.Next(1, 4);
				if (timeValue == 1)
				{
					arrayFigure[i] = CreateNewCircle();
				}
				else if (timeValue == 2)
				{
					arrayFigure[i] = CreateNewSquare();
				}
				else
				{
					arrayFigure[i] = CreateNewTriangle();
				}
			}

			enumerableFigure = new ObjectsEnumerable<AbstractFigure>(arrayFigure);
		}

		private AbstractFigure CreateNewSquare()
		{
			Point[] arrPoint = new Point[4];
			arrPoint[0] = new Point(rn.Next(-30, 0), rn.Next(0, 30));
			arrPoint[1] = new Point(rn.Next(0, 31), rn.Next(0, 30));
			arrPoint[2] = new Point(rn.Next(0, 31), rn.Next(-30, 0));
			arrPoint[3] = new Point(rn.Next(-30, 0), rn.Next(-30, 0));

			return new Square(arrPoint);
		}

		private AbstractFigure CreateNewTriangle()
		{
			Point[] arrPoint = new Point[3];
			arrPoint[0] = new Point(rn.Next(0, 31), rn.Next(-30, 0));
			arrPoint[1] = new Point(rn.Next(0, 31), rn.Next( 0, 30));
			arrPoint[2] = new Point(rn.Next(-30, 0), rn.Next( 0, 30));

			return new Triangle(arrPoint);
		}

		private AbstractFigure CreateNewCircle()
		{
			return new Circle(new Point(rn.Next(-30, 30), rn.Next(-30, 30)), rn.Next(1, 41));
		}

		private void Print()
		{
			foreach (AbstractFigure Figure in enumerableFigure)
			{
				Console.WriteLine(Figure.ToString());
				Console.WriteLine();
			}
		}

		[SetUp]
		public void SetUp()
		{
			Generate();
		}

		[Test]
		public void PrintListFigure()
		{
			foreach (AbstractFigure p in enumerableFigure)
			{
				Console.WriteLine(p.ToString());
				Console.WriteLine();
			}
		}

		[Test]
		public void SortFigureAscendingArea()
		{
			Array.Sort(arrayFigure);
			Console.WriteLine("Сортировка по площади");
			Print();
		}

		[Test]
		public void SortFigureAscendingPerimetr()
		{
			Array.Sort(arrayFigure, AbstractFigure.SortPerimetrAscending());
			Console.WriteLine("Сортировка по периметру");
			Print();
		}
	}
}