using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulatorUI
{
    public partial class DrawingArea : Panel
    {
        #region properties
        //public List<IDrawable> _objectsToDraw = null;
        public static List<IDrawable> ObjectsToDraw = new List<IDrawable>();
        //{
        //    get
        //    {
        //        if(_objectsToDraw == null)
        //        {
        //            _objectsToDraw = new List<IDrawable>();
        //        }
        //        return _objectsToDraw;
        //    }
        //}
        #endregion

        public DrawingArea()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //BackgroundImage = Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Cars\\SimulatorUI\\Images\\Strada4.png");
            //BackgroundImageLayout = ImageLayout.Center;

            UpdateStyles();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            lock (ObjectsToDraw)
            {
                foreach (var drawable in ObjectsToDraw)
                {
                    drawable.Draw(e.Graphics);
                }
            }
        }
    }
}
