using System;
using System.Collections;

namespace ClassLibrary.Figure
{
	public partial class AbstractFigure : IComparable
	{
		public int CompareTo(object obj)
		{
			AbstractFigure figure = obj as AbstractFigure;

			if (Area > figure.Area)
				return 1;
			else if (figure.Area > Area)
				return -1;
			else
				return 0;
		}

		private class SortAreaDescendingHelper : IComparer
		{
			public int Compare(object obj1, object obj2)
			{
				AbstractFigure figure1 = obj1 as AbstractFigure;
				AbstractFigure figure2 = obj2 as AbstractFigure;

				if (figure2.Area > figure2.Area)
					return 1;
				else if (figure1.Area > figure2.Area)
					return -1;
				else
					return 0;
			}
		}

		private class SortPerimetrDescendingHelper : IComparer
		{
			public int Compare(object obj1, object obj2)
			{
				AbstractFigure figure1 = obj1 as AbstractFigure;
				AbstractFigure figure2 = obj2 as AbstractFigure;

				if (figure2.Perimetr > figure1.Perimetr)
					return 1;
				else if (figure1.Perimetr > figure2.Perimetr)
					return -1;
				else
					return 0;
			}
		}

		private class SortPerimetrAscendingHelper : IComparer
		{
			public int Compare(object obj1, object obj2)
			{
				AbstractFigure figure1 = obj1 as AbstractFigure;
				AbstractFigure figure2 = obj2 as AbstractFigure;

				if (figure1.Perimetr > figure2.Perimetr)
					return 1;
				else if (figure2.Perimetr > figure1.Perimetr)
					return -1;
				else
					return 0;
			}
		}

		public static IComparer SortAreaDescending()
		{
			return new SortAreaDescendingHelper();
		}

		public static IComparer SortAreaAscending()
		{
			return new SortPerimetrAscendingHelper();
		}

		public static IComparer SortPerimetrDescending()
		{
			return new SortPerimetrDescendingHelper();
		}
	}
}
