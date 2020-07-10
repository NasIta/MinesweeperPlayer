using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using MinesweeperPlayer.Vision;

namespace MinesweeperPlayer
{
	public partial class MainForm : Form
	{
		public static Rectangle WorkingArea {get; set;}
		public static Bitmap FieldScreen {get; set;}
		public static Color[,][,] ColorMaps {get; set;}
		public static Size FieldSize {get; set;}
		public static char[,] ValueMap {get; set;}
		
		private BorderForm _borderForm;
		
		public MainForm()
		{
			InitializeComponent();
		}
		
		private void Button1Click(object sender, EventArgs eventArgs)
		{
			if (_borderForm == null)
			{
				_borderForm = new BorderForm();
				_borderForm.Show();
				
				_borderForm.FormClosed += (s, e) => 
				{
					_borderForm.Dispose();
					_borderForm = null;
					
					button1.Text = "Set borbers";
				};
				
				button1.Text = "Done";
			}
			else
			{
				_borderForm.Close();
			}
		}
		
		private void Button4Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		private void MainFormLoad(object sender, EventArgs e)
		{
			Top = Screen.PrimaryScreen.Bounds.Height - Height;;
			Left = 45;
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			if (BorderForm.borderSeted) 
			{
				if (_borderForm != null)
				{
					_borderForm.Close();
				}
				
				var d1 = DateTime.Now;
				
				FieldScreen = CellFinder.GetNormilizedScreen();
				FieldSize = FieldScreen.GetFieldSize();
				ValueMap = FieldScreen.GetCellColorMaps().GetCellValues();
				
				FieldScreen.Dispose();
				FieldScreen = null;
				
				var d2 = DateTime.Now;
				
				var checkMap = new System.Text.StringBuilder("");
				
				for (int y = 0; y < FieldSize.Height; y++) 
				{
					for (int x = 0; x < FieldSize.Width; x++) 
					{
						checkMap.Append(ValueMap[x, y] + "  ");
					}
					
					checkMap.Append("\n");
				}
				
				checkMap.Append("\n\nCalculated for " + (d2 - d1).TotalMilliseconds.ToString("0.000") + " milliseconds");
				
				MessageBox.Show(checkMap.ToString());
			}
		}
	}
}
