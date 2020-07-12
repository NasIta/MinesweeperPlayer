using System;
using System.Collections.Generic;
using System.Drawing;

namespace MinesweeperPlayer.Analysis
{
	public class Cell
	{
		public static Size Size {get; set;}
		public static Cell[,] Field {get; set;}
		public static List<Cell> ClosedNumbers {get; set;}
		
		public char Value {get; private set;}
		public int NumberValue;
		public bool isNumber {get; private set;}
		public readonly Point Location;
		
		public Cell(char Value, int X, int Y)
		{
			SetValue(Value);
			
			Location = new Point(X, Y);
		}
		
		public void SetValue(char Value)
		{
			this.Value = Value;
			
			if (Int32.TryParse(Value.ToString(), out NumberValue))
			{
				isNumber = true;
			}
			else
			{
				isNumber = false;
			}
		}
	}
}
