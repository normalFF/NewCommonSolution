using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.HierarchyBaseObjects
{
	public class Car : Transport
	{
		private string _brand;

		public string Brand
		{
			get
			{
				return _brand;
			}
		}

		public Car(string brand, double maxSpeed, int numbersSeats, double mass) : base(mass, maxSpeed, numbersSeats)
		{
			_brand = brand ?? throw new ArgumentNullException("Brand не может быть null");
		}

		public override void TravelType()
		{
			Console.WriteLine("Moves in ordinary roads");
		}

		public override string ToString()
		{
			return $"Марка: {Brand}" + base.ToString();
		}
	}
}
