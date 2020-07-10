/*
 * Created by SharpDevelop.
 * User: NasIta
 * Date: 02.07.2020
 * Time: 21:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace MinesweeperPlayer
{
	partial class BorderForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox _picture;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this._picture = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this._picture)).BeginInit();
			this.SuspendLayout();
			// 
			// _picture
			// 
			this._picture.BackColor = System.Drawing.Color.White;
			this._picture.Dock = System.Windows.Forms.DockStyle.Fill;
			this._picture.Location = new System.Drawing.Point(0, 0);
			this._picture.Name = "_picture";
			this._picture.Size = new System.Drawing.Size(722, 568);
			this._picture.TabIndex = 0;
			this._picture.TabStop = false;
			this._picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1MouseDown);
			this._picture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1MouseMove);
			this._picture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox1MouseUp);
			// 
			// BorderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(722, 568);
			this.Controls.Add(this._picture);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "BorderForm";
			this.Opacity = 0.35D;
			this.Text = "BorderForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this._picture)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
