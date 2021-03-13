using System;
using System.Collections;

namespace ClassLibrary.HierarchyBaseObjects
{
	public partial class People : IComparable
	{
		public int CompareTo(object obj)
		{
			People p = obj as People;
			return string.Compare(this.Name, p.Name);
		}

		class sortNameDescendingHelper : IComparer
		{
			int IComparer.Compare(object objOne, object objTwo)
			{
				People p1 = objOne as People;
				People p2 = objTwo as People;

				return string.Compare(p2.Name, p1.Name);
			}
		}

		class sortAgeAscendingHelper : IComparer
		{
			int IComparer.Compare(object objOne, object objTwo)
			{
				People p1 = objOne as People;
				People p2 = objTwo as People;

				if (p1.Age > p2.Age)
					return 1;
				if (p2.Age > p1.Age)
					return -1;

				return 0;
			}
		}

		class sortAgeDescendingHelper : IComparer
		{
			int IComparer.Compare(object objOne, object objTwo)
			{
				People p1 = objOne as People;
				People p2 = objTwo as People;

				if (p2.Age > p1.Age)
					return 1;
				if (p1.Age > p2.Age)
					return -1;

				return 0;
			}
		}

		class sortMassAscendingHelper : IComparer
		{
			int IComparer.Compare(object objOne, object objTwo)
			{
				People p1 = objOne as People;
				People p2 = objTwo as People;

				if (p1.Mass > p2.Mass)
					return 1;
				if (p2.Mass > p1.Mass)
					return -1;

				return 0;
			}
		}

		class sortMassDescendingHelper : IComparer
		{
			int IComparer.Compare(object objOne, object objTwo)
			{
				People p1 = objOne as People;
				People p2 = objTwo as People;

				if (p2.Mass > p1.Mass)
					return 1;
				if (p1.Mass > p2.Mass)
					return -1;

				return 0;
			}
		}

		public static IComparer sortNameDescending()
		{
			return new sortNameDescendingHelper();
		}

		public static IComparer sortAgeAscending()
		{
			return new sortAgeAscendingHelper();
		}

		public static IComparer sortAgeDescending()
		{
			return new sortAgeDescendingHelper();
		}

		public static IComparer sortMassAscending()
		{
			return new sortMassAscendingHelper();
		}

		public static IComparer sortMassDescending()
		{
			return new sortMassDescendingHelper();
		}
	}
}
