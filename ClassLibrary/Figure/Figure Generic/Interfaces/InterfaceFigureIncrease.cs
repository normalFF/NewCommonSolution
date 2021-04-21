namespace ClassLibrary.Figure.Generic
{
	public interface IFigureIncrease<in T> where T : AbstractFigure
	{
		public void IncreasePointPosition(T obj, double coefficient);
	}
}
