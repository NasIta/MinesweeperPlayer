using System;
using System.Drawing;
using System.Windows.Forms;

namespace MinesweeperPlayer
{
	public partial class BorderForm : Form
	{
		public static bool borderSeted;
		public static bool borderChanged;
		
		private Rectangle _rect;
		private Bitmap _bmp;
		private Graphics _graph;
		
		public BorderForm()
		{
			InitializeComponent();
			
			_bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			_graph = Graphics.FromImage(_bmp);
		}
		
		private void PictureBox1MouseDown(object sender, MouseEventArgs e)
		{
			_rect = new Rectangle(e.Location.X, e.Location.Y, 0, 0);
		}
		
		private void PictureBox1MouseUp(object sender, MouseEventArgs e)
		{
			borderSeted = true;
			borderChanged = true;
		}
		
		private void PictureBox1MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left) 
			{
				Draw(e);
			}
		}

		private void Draw(MouseEventArgs e)
		{
			_graph.Clear(Color.White);
			
			MainForm.WorkingArea = new Rectangle(Math.Min(_rect.X, e.X), Math.Min(_rect.Y, e.Y), Math.Max(_rect.X, e.X) - Math.Min(_rect.X, e.X), Math.Max(_rect.Y, e.Y) - Math.Min(_rect.Y, e.Y));
			
			_graph.FillRectangle(new SolidBrush(Color.LightGreen), MainForm.WorkingArea);
			
			_picture.Image = _bmp;
		}
	}
}
