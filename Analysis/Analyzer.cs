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
			
			foreach (var cell in GetToCloseNumberCells())
				foreach (var near in cell.GetNearest('C'))
					near.Close();
			
			foreach (var cell in GetToOpenNumberCells())
				foreach (var near in cell.GetNearest('C'))
					near.Open();
			
			if (Clicked == 0)
			{
				
			}
		}
		
		private static List<Cell> GetToOpenNumberCells()
		{
			var openedNumbers = new List<Cell>();
			
			for (int y = 1; y < MainForm.FieldSize.Height - 1; y++)
			{
				for (int x = 0; x < MainForm.FieldSize.Width; x++)
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
		
		private static List<Cell> GetToCloseNumberCells()
		{
			var closedNumbers = new List<Cell>();
			
			for (int y = 1; y < MainForm.FieldSize.Height - 1; y++)
			{
				for (int x = 0; x < MainForm.FieldSize.Width; x++)
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
					
					if(xcurrent < 0 || ycurrent < 0 || xcurrent >= MainForm.FieldSize.Width || ycurrent >= MainForm.FieldSize.Height) continue;
					
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
