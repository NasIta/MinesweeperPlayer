using System;
using System.Collections.Generic;
using MinesweeperPlayer.MouseControll;

namespace MinesweeperPlayer.Analysis
{
	public static class Analyzer
	{
		public static int Clicked {get; set;}
		
		public static void Solve()
		{
			Clicked = 0;
			
			var closed = GetClosedNumberCells();
			
			foreach (var cell in closed)
				foreach (var near in cell.GetNearest('C', 'F'))
					near.Close();
			
			var opened = GetOpenedNumberCells();
			
			foreach (var cell in opened)
				foreach (var near in cell.GetNearest('C'))
					near.Open();
		}
		
		private static List<Cell> GetOpenedNumberCells()
		{
			var openedNumbers = new List<Cell>();
			
			for (int y = 1; y < MainForm.FieldSize.Height - 1; y++)
			{
				for (int x = 1; x < MainForm.FieldSize.Width - 1; x++)
				{
					if (Cell.Field[x, y].isNumber)
					{
						if (Cell.Field[x, y].GetNearest('F').Count == Cell.Field[x, y].NumberValue && Cell.Field[x, y].GetNearest('C').Count > 0) 
						{
							openedNumbers.Add(Cell.Field[x, y]);
						}
					}
				}
			}
			
			return openedNumbers;
		}
		
		private static List<Cell> GetClosedNumberCells()
		{
			var closedNumbers = new List<Cell>();
			
			for (int y = 1; y < MainForm.FieldSize.Height - 1; y++)
			{
				for (int x = 1; x < MainForm.FieldSize.Width - 1; x++)
				{
					if (Cell.Field[x, y].isNumber)
					{
						if (Cell.Field[x, y].GetNearest('C', 'F').Count == Cell.Field[x, y].NumberValue) 
						{
							closedNumbers.Add(Cell.Field[x, y]);
						}
					}
				}
			}
			
			return closedNumbers;
		}
		
		private static List<Cell> GetNearest(this Cell cell, params char[] values)
		{
			var nearestClosed = new List<Cell>();
			
			for (int dy = -1; dy < 2; dy++)
			{
				for (int dx = -1; dx < 2; dx++)
				{
					if (dx == 0 && dy == 0) continue;
					
					int xcurrent = cell.Location.X + dx;
					int ycurrent = cell.Location.Y + dy;
					
					for (int i = 0; i < values.Length; i++) 
					{
						if (Cell.Field[xcurrent, ycurrent].Value == values[i])
						{
							nearestClosed.Add(Cell.Field[xcurrent, ycurrent]);
						}
					}
					
				}
			}
			
			return nearestClosed;
		}
		
		private static void Close(this Cell cell)
		{
			if (cell.Value == 'C')
			{
				cell.SetValue('F');
				Clicked++;
				MouseController.Close(cell.Location.X, cell.Location.Y);
			}
		}
		
		private static void Open(this Cell cell)
		{
			if (cell.Value == 'C')
			{
				cell.SetValue('U');
				Clicked++;
				MouseController.Open(cell.Location.X, cell.Location.Y);
			}
		}
	}
}
