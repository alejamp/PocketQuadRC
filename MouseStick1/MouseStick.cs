using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace NSMouseStick
{
	public enum dir {N, NE, E, SE, S, SW, W, NW};
	public delegate void MouseStickMovedEventHandler(object sender, MouseStickEventArgs e);
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	/// 
	[DefaultEventAttribute("MouseStickMoved")]
	public class MouseStick : System.Windows.Forms.Control
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public event MouseStickMovedEventHandler MouseStickMoved;
		bool IsActive;
		System.Windows.Forms.Timer frameTimer;

		
		public MouseStick()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.MouseMove += new MouseEventHandler(MouseStick_MouseMove);
            this.MouseDown += new MouseEventHandler(MouseStick_MouseDown);
            this.MouseUp += new MouseEventHandler(MouseStick_MouseUp);
			this.BackColor = Color.Black;
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.Cursor = Cursors.Cross;
			this.needleColor = Color.Red;
			this.needleWidth = 3;
			this.lineEnd = LineCap.Round;
			this.IsActive = false;
			this.MouseEnter += new EventHandler(MouseStick_MouseEnter);
			this.MouseLeave += new EventHandler(MouseStick_MouseLeave);
			this.frameTimer = new Timer();
			this.frameTimer.Interval = 33;
			this.frameTimer.Enabled = true;
			this.frameTimer.Tick +=new EventHandler(frameTimer_Tick);
			this.mouseX = this.Width / 2;
			this.mouseY = this.Height / 2;

		}

        void MouseStick_MouseUp(object sender, MouseEventArgs e)
        {
            this.isMouseDown = false;
        }

        void MouseStick_MouseDown(object sender, MouseEventArgs e)
        {
            this.isMouseDown = true;
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region Event Handlers

		private void MouseStick_MouseEnter(object o, EventArgs e)
		{
				IsActive = true;
		}

		private void MouseStick_MouseLeave(object o, EventArgs e)
		{
			IsActive = false;
			radius = 0;
			mouseX = this.Width / 2;
			mouseY = this.Height / 2;
			if(MouseStickMoved != null)
				MouseStickMoved(this, new MouseStickEventArgs(this.radius, this.direction, 0, 0));

		}

		private void frameTimer_Tick(object o, EventArgs e)
		{
			this.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			SolidBrush needleBrush = new SolidBrush(needleColor);
			Pen pLine = new Pen(needleBrush, needleWidth);
			pLine.EndCap = lineEnd;
			pLine.StartCap = LineCap.Round;

			//Assign the control's region
			GraphicsPath gP = new GraphicsPath();
			gP.AddEllipse(this.ClientRectangle);
			this.Region = new Region(gP);

			//Draw the joystick.
			g.DrawLine(pLine,this.Width / 2, this.Height / 2,
				mouseX, mouseY);
			base.OnPaint(e);
		}

	
		private void MouseStick_MouseMove(object o, System.Windows.Forms.MouseEventArgs e)
		{
			if (IsActive)
			{
				int x_Val,y_Val;
				//Translating mouse motion on the control
				//To a 30 x 30 cartesian grid.
				y_Val = (e.Y * 31 / this.Height) - 15;
				x_Val = (e.X * 31 / this.Width) - 15;
				y_Val = -y_Val;

                xMagnitud = (float)(e.X - this.Width / 2) / (this.Width/2) ;
                yMagnitud = (float)(e.Y - this.Height / 2) / (this.Height/2);

				//Setting up to raise the MouseStickMoved Event
				direction = GetDirection(x_Val, y_Val);
				radius = (int)Math.Sqrt(y_Val * y_Val + x_Val * x_Val);
				if (radius > 15) radius = 15;
				mouseX = e.X;
				mouseY = e.Y;
			}
			else
			{
				radius = 0;
				mouseX = this.Width / 2;
				mouseY = this.Height / 2;
			}
		
			//The event is raised after the control is redrawn.
			if(MouseStickMoved != null && isMouseDown)
				MouseStickMoved(this, new MouseStickEventArgs(this.radius, this.direction, xMagnitud, yMagnitud));
		}

		#endregion

		private dir GetDirection(float x, float y)
		{
			//Changing cartesian coordinates from control surface to  
			//more usable polar coordinates
			double theta;
				if(x >= 0 && y > 0)
					theta = (Math.Atan(y/ x) * (180 /Math.PI));
				else if(x < 0)
					theta = ((Math.PI + Math.Atan(y / x)) * (180 /Math.PI));
				else theta = (((2*Math.PI) + Math.Atan(y / x)) * (180 /Math.PI));
			
			//Changing from degrees to direction.
			if(theta <= 26 || theta > 341)
				return dir.E;
			else if (theta > 26 && theta <=71)
				return dir.NE;
			else if (theta > 71 && theta <= 116)
				return dir.N;
			else if (theta > 116 && theta <= 161)
				return dir.NW;
			else if (theta > 161 && theta <= 206)
				return dir.W;
			else if (theta > 206 && theta <= 251)
				return dir.SW;
			else if (theta > 251 && theta <= 296)
				return dir.S;
			else return dir.SE;
		}

		#region Properties

		[CategoryAttribute("Appearance"),
		DescriptionAttribute("Shape of the Needle End.")]
		public LineCap LineEnd
		{
			get {return lineEnd;}
			set 
			{
				lineEnd = value;
				this.Invalidate();
			}
		}
		
		[CategoryAttribute("Appearance"),
		DescriptionAttribute("Color of the needle.")]
		public Color NeedleColor
		{
			get{return needleColor;}
			set
			{
				needleColor = value;
				this.Invalidate();
			}
		}
		
		[CategoryAttribute("Appearance"),
		DescriptionAttribute("Width of the needle.")]
		public int NeedleWidth
		{
			get{return needleWidth;}
			set
			{
				needleWidth = value;
				this.Invalidate();
			}
		}
		#endregion

		#region Fields
		int radius;
		int mouseX, mouseY, needleWidth;
        float xMagnitud = 0;  // 0 .. 1
        float yMagnitud = 0;  // 0 .. 1
		Color needleColor;
		LineCap lineEnd;
		
		dir direction;
		#endregion


        public bool isMouseDown { get; set; }
    }

	#region MouseStickEventArgs Class

	public class MouseStickEventArgs : System.EventArgs
	{
		public MouseStickEventArgs(int Radius, dir Direction, float xm, float ym)
		{
			direction = Direction;
			R = Radius;
            this.xm = xm;
            this.ym = ym;
		}

        public float xMag
        {
            get { return this.xm; }
        }
        public float yMag
        {
            get { return this.ym; }
        }

		public int Radius
		{
			get{return R;}
		}
		public dir Direction
		{
			get{return direction;}
		}

		int R;
        dir direction;
        float xm, ym;
	}
	#endregion
}
