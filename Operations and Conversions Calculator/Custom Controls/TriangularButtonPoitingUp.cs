using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Operations_and_Conversions_Calculator.Custom_Controls
{
    public partial class TriangularButtonPoitingUp : Button
    {
        public TriangularButtonPoitingUp()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float h = this.Height;
            float w = this.Width;

            PointF[] pts = new PointF[] { new PointF(w / 2, 0), new PointF(0, w), new PointF(w, h) };
            g.FillPolygon(new SolidBrush(this.BackColor), pts);
            GraphicsPath gp = new GraphicsPath();
            gp.AddPolygon(pts);

            this.Region = new Region(gp);
            base.OnPaint(pe);
        }
    }
}
