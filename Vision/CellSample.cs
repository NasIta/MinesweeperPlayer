using System;
using System.Drawing;

namespace MinesweeperPlayer.Vision
{
	public class CellSample
	{
		public Bitmap Image {get; set;}
		public char Value {get; set;}
		public Color[,] ColorMap {get; set;}
	}
}
