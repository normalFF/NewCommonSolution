using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.HierarchyBaseObjects
{
	public abstract class Transport : BaseObject
	{
		public double MaxSpeed
		{
			get; protected set;
		}
		public int NumbersSeats
		{
			get; protected set;
		}

		public Transport(double mass, double maxSpeed, int numberSeats) : base(mass)
		{
			MaxSpeed = maxSpeed;

			if (numberSeats < 0)
				throw new ArgumentOutOfRangeException("Количество мест не может быть отрицательным числом");
			NumbersSeats = numberSeats;
		}

		public override string ToString()
		{
			return $"\nМасса: {Mass}\nМаксимальная скорость: {MaxSpeed}\nКоличество мест: {NumbersSeats}\n";
		}

		public abstract void TravelType();
	}
}
