using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.HierarchyBaseObjects
{
	public class Train : Transport
	{
		public string Manufacturer
		{
			get; protected set;
		}
		public string FuelType
		{
			get; protected set;
		}

		public Train(string manufacturer, string fuel, double maxSpeed, int numbersSeats, double mass) : base(mass, maxSpeed, numbersSeats)
		{
			Manufacturer = manufacturer ?? throw new ArgumentNullException("Manufacturer не может быть null");
			FuelType = fuel ?? throw new ArgumentNullException("FuelType не может быть null");
		}

		public override void TravelType()
		{
			Console.WriteLine("Moves in special ways");
		}

		public override string ToString()
		{
			return $"Компания: {Manufacturer}\nТип топлива: {FuelType}" + base.ToString();
		}
	}
}
