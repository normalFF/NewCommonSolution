using NUnit.Framework;
using System;
using ClassLibrary;
using ClassLibrary.Figure;
using System.Collections;

namespace Tests
{
	class TestSortFigure
	{
		Random rn = new Random();
		AbstractFigure[] ArrayFigure;
		ObjectsEnumerable<AbstractFigure> IEnumerableFigure;

		private void Generate()
		{
			ArrayFigure = new AbstractFigure[50];

			for (int i = 0; i < ArrayFigure.Length; i++)
			{
				int TimeValue = rn.Next(1, 4);
				if (TimeValue == 1)
				{
					ArrayFigure[i] = CreateNewCircle();
				}
				else if (TimeValue == 2)
				{
					ArrayFigure[i] = CreateNewSquare();
				}
				else
				{
					ArrayFigure[i] = CreateNewTriangle();
				}
			}

			IEnumerableFigure = new ObjectsEnumerable<AbstractFigure>(ArrayFigure);
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
			foreach (AbstractFigure Figure in IEnumerableFigure)
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
			foreach (AbstractFigure p in IEnumerableFigure)
			{
				Console.WriteLine(p.ToString());
				Console.WriteLine();
			}
		}

		[Test]
		public void SortFigureAscendingArea()
		{
			Array.Sort(ArrayFigure);
			Console.WriteLine("Сортировка по площади");
			Print();
		}

		[Test]
		public void SortFigureAscendingPerimetr()
		{
			Array.Sort(ArrayFigure, AbstractFigure.SortPerimetrAscending());
			Console.WriteLine("Сортировка по периметру");
			Print();
		}
	}
}