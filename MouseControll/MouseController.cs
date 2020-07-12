using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MinesweeperPlayer.MouseControll
{
	public delegate void ClickDelegate(int xcell, int ycell);
	
	public static class MouseController
	{
   		private const int MOUSEEVENTF_LEFTDOWN = 0x02;
	   	private const int MOUSEEVENTF_LEFTUP = 0x04;
	   	private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
	   	private const int MOUSEEVENTF_RIGHTUP = 0x10;
	   	
	   	public static List<ClickDelegate> TODO;
	   	
	   	static MouseController()
	   	{
	   		TODO = new List<ClickDelegate>();
	   	}
	   	
	   	public static void Close(int xcell, int ycell)
	   	{
	   		Click(MainForm.WorkingArea.X + xcell * Analysis.Cell.Size.Width + Analysis.Cell.Size.Width / 2, MainForm.WorkingArea.Y + ycell * Analysis.Cell.Size.Height + Analysis.Cell.Size.Height / 2, MouseButtons.Right);
	   	}
	   	
	   	public static void Open(int xcell, int ycell)
	   	{
	   		Click(MainForm.WorkingArea.X + xcell * Analysis.Cell.Size.Width + Analysis.Cell.Size.Width / 2, MainForm.WorkingArea.Y + ycell * Analysis.Cell.Size.Height + Analysis.Cell.Size.Height / 2, MouseButtons.Left);
	   	}
	   	
	   	[DllImport("user32.dll", CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
   		private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
	   	
		private static void Click(int X, int Y, MouseButtons mouseButton)
		{
			Cursor.Position = new Point(X, Y);
			
			if (mouseButton == MouseButtons.Left) 
			{
				mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)X, (uint)Y, 0, 0);
			}
			else if(mouseButton == MouseButtons.Right)
			{
				mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, (uint)X, (uint)Y, 0, 0);
			}
		}
	}
}
