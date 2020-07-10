using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MinesweeperPlayer.Vision
{
	public static class Screener
	{
		private static Bitmap _bmp;
		private static Graphics _graph;
		
		public static Bitmap MakeFieldScreenshot()
		{
			_bmp = new Bitmap(MainForm.WorkingArea.Width, MainForm.WorkingArea.Height);
			_graph = Graphics.FromImage(_bmp as Image);
			
			_graph.CopyFromScreen(MainForm.WorkingArea.X, MainForm.WorkingArea.Y, 0, 0, _bmp.Size);
			
//			var date = DateTime.Now.ToString("MMddyyHmmss");
//			_bmp.Save(@"C:\Users\NasIta\Desktop\printscreen"+date+".bmp", ImageFormat.Jpeg);
			
			return _bmp;
		}
		
		public static Bitmap MakeFullScreenshot()
		{
			_bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			_graph = Graphics.FromImage(_bmp as Image);
			
			_graph.CopyFromScreen(0, 0, 0, 0, _bmp.Size);
			
			return _bmp;
		}
	}
}
