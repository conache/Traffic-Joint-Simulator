using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorUI
{
    class DrawableTrafficLight:IDrawable
    {
        public State Color { get; set; }
        public Category Position { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        
        public DrawableTrafficLight(Category position, State color, int x, int y)
        {
            this.Color = color;
            this.Position = position;
            this.X = x;
            this.Y = y;
        }

        public void ChangeNextColor()
        {
            if (Color == State.Red)
                Color = State.Green;
            else
                Color++;
        }

        public void Draw(System.Drawing.Graphics g)
        {
            g.DrawImage(ImageManager.GetImage(Position, Color), X, Y);
        }
    }
}
