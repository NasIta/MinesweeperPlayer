using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;

namespace MinesweeperPlayer.Vision
{
	public static class CellFinder
	{
		private const float Epsilon = 0.00001f;
		private const int ColorMapSize = 4;
		
		private static float _closedBrightness;					// original color of closed cell
		private static int _delta;								// used for jumping over cell borders 
		private static bool _isBorder;
		
		private static CellSample[] _samples;
		
		static CellFinder()
		{
			LoadSamples();
		}
		
		public static Bitmap GetNormilizedScreen()
		{
			using(var fullBmp = Screener.MakeFullScreenshot())		
			{
				_closedBrightness = fullBmp.GetPixel(MainForm.WorkingArea.X, MainForm.WorkingArea.Y).GetBrightness();
				
				int x = MainForm.WorkingArea.X;
				while (Math.Abs(fullBmp.GetPixel(x - 1, MainForm.WorkingArea.Y).GetBrightness() - _closedBrightness) < Epsilon) 
					x--;
				
				int y = MainForm.WorkingArea.Y;
				while (Math.Abs(fullBmp.GetPixel(MainForm.WorkingArea.X, y - 1).GetBrightness() - _closedBrightness) < Epsilon) 
					y--;
				
				int w = MainForm.WorkingArea.Width;
				while (Math.Abs(fullBmp.GetPixel(MainForm.WorkingArea.X + w + 1, MainForm.WorkingArea.Y).GetBrightness() - _closedBrightness) < Epsilon) 
					w++;
				
				int h = MainForm.WorkingArea.Height;
				while (Math.Abs(fullBmp.GetPixel(MainForm.WorkingArea.X, MainForm.WorkingArea.Y + h + 1).GetBrightness() - _closedBrightness) < Epsilon) 
					h++;
				
				MainForm.WorkingArea = new Rectangle(x - 2, y - 2, w + MainForm.WorkingArea.X - x + 5, h + MainForm.WorkingArea.Y - y + 5);
			}
			
			return Screener.MakeFieldScreenshot();
		}
		
		public static Size GetFieldSize(this Bitmap bmp)
		{
			var size = new Size(1, 1);
			
			_isBorder = false;
			_delta = 0;
			
			const int _bias = 5;
			
			for (int x = _bias; x < bmp.Width - _bias; x++) 
			{				
				if(IsNewCell(x, _bias, bmp))
					size = new Size(size.Width + 1, 1);
			}
			
			for (int y = _bias; y < bmp.Height - _bias; y++) 
			{
				if(IsNewCell(_bias, y, bmp))
					size = new Size(size.Width, size.Height + 1);
			}
			
			return size;
		}
		
		public static Color[,][,] GetCellColorMaps(this Bitmap bmp)
		{
			int _width = MainForm.FieldSize.Width;
			int _height = MainForm.FieldSize.Height;
			
			var colorMaps = new Color[_width, _height][,];
			
			for (int ycell = 0; ycell < _height; ycell++) 
			{
				for (int xcell = 0; xcell < _width; xcell++) 
				{
					float step = bmp.Width / _width / (ColorMapSize + 1f);
					
					colorMaps[xcell, ycell] = new Color[ColorMapSize, ColorMapSize];
					
					for (int y = 0; y < ColorMapSize; y++) 
					{
						for (int x = 0; x < ColorMapSize; x++) 
						{
							float stepX = step * (x + 1);
							float stepY = step * (y + 1);
							
							int stepFlooredX = Convert.ToInt32(Math.Floor(stepX));
							int stepFlooredY = Convert.ToInt32(Math.Floor(stepY));
							
							colorMaps[xcell, ycell][x, y] = bmp.GetPixel(xcell * bmp.Width / _width + stepFlooredX, ycell * bmp.Height / _height + stepFlooredY);
						}
					}
				}
			}
			
			return colorMaps;
		}
		
		public static char[,] GetCellValues(this Color[,][,] colorMaps)
		{
			var valueMap = new char[MainForm.FieldSize.Width, MainForm.FieldSize.Height];
			
			for (int y = 0; y < MainForm.FieldSize.Height; y++) 
			{
				for (int x = 0; x < MainForm.FieldSize.Width; x++) 
				{
					valueMap[x, y] = colorMaps[x, y].GetCellValue();
				}
			}
			
			return valueMap;
		}
		
		public static char GetCellValue(this Bitmap bmp)
		{
			return bmp.GetCellColorMap().GetCellValue();
		}
		
		private static char GetCellValue(this Color[,] colorMap)
		{
			float min = float.MaxValue;
			char value = '_';
			
			foreach(var sample in _samples)
			{
				int result = CompareColorMaps(sample.ColorMap, colorMap);
				
				if (result < min) 
				{
					min = result;
					value = sample.Value;
				}
			}
			
			return value;
		}
		
		private static Color[,] GetCellColorMap(this Bitmap bmp)
		{
			float step = bmp.Width / (ColorMapSize + 1f);
			
			var map = new Color[ColorMapSize, ColorMapSize];
			
			for (int y = 0; y < ColorMapSize; y++) 
			{
				for (int x = 0; x < ColorMapSize; x++) 
				{
					float stepX = step * (x + 1);
					float stepY = step * (y + 1);
					
					int stepFlooredX = Convert.ToInt32(Math.Floor(stepX));
					int stepFlooredY = Convert.ToInt32(Math.Floor(stepY));
					
					map[x, y] = bmp.GetPixel(stepFlooredX, stepFlooredY);
				}
			}
			
			return map;
		}
		
		private static int CompareColorMaps(Color[,] map1, Color[,] map2)
		{
			int sumOfDifferenses = 0;
			
			for (int y = 0; y < ColorMapSize; y++) 
			{
				for (int x = 0; x < ColorMapSize; x++) 
				{
					sumOfDifferenses += Math.Abs(map1[x, y].R - map2[x, y].R);
					sumOfDifferenses += Math.Abs(map1[x, y].G - map2[x, y].G);
					sumOfDifferenses += Math.Abs(map1[x, y].B - map2[x, y].B);
				}
			}
			
			return sumOfDifferenses;
		}
		
		private static bool IsNewCell(int x, int y, Bitmap bmp)
		{
			if (Math.Abs(bmp.GetPixel(x, y).GetBrightness() - _closedBrightness) < Epsilon)
			{
				_delta++;
				
				if (_isBorder && _delta > 2) 
				{
					_isBorder = false;
					_delta = 0;
					
					return true;
				}
			}
			else
			{
				_isBorder = true;
				_delta = 0;
			}
			
			return false;
		}

		private static void LoadSamples()
		{
			_samples = new CellSample[10];
			
			var paths = Directory.GetFiles(Application.StartupPath + @"\Samples");
			
			for (int i = 0; i < paths.Length; i++) 
			{
				_samples[i] = new CellSample();
				
				_samples[i].Image = (Bitmap)Image.FromFile(paths[i]);		
				_samples[i].Value = paths[i][paths[i].Length - 5];			// getting name of image without extention
				_samples[i].ColorMap = _samples[i].Image.GetCellColorMap();
			}
		}
	}
}
