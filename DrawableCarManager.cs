using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorUI
{
    

    sealed class DrawableCarManager
    {
        private static readonly DrawableCarManager instance = new DrawableCarManager();
        private struct DefaultCarConfig
        {
            public int X;
            public int Y;
            public int Speed;
            public State State;
            public DefaultCarConfig(int x, int y, State state, int speed = 2)
            {
                this.X = x;
                this.Y = y;
                this.State = state;
                this.Speed = speed;
            }
        }
        private static Dictionary<TrafficLights.Joint.RoadLocation, DefaultCarConfig> rightLaneCoords =
            new Dictionary<TrafficLights.Joint.RoadLocation, DefaultCarConfig>()
        {
            {TrafficLights.Joint.RoadLocation.Up, new DefaultCarConfig(870, 0, State.MovingDown)},
            {TrafficLights.Joint.RoadLocation.Right, new DefaultCarConfig(1790, 500, State.MovingLeft)},
            {TrafficLights.Joint.RoadLocation.Down, new DefaultCarConfig(930, 1195, State.MovingUp)},
            {TrafficLights.Joint.RoadLocation.Left, new DefaultCarConfig(0, 620, State.MovingRight)}
        };
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static DrawableCarManager()
        {
        }

        private DrawableCarManager()
        {
        }

        public static DrawableCarManager Instance
        {
            get
            {
                return instance;
            }
        }

        public static DrawableCar CreateNewCar(TrafficLights.Joint.RoadLocation road)
        {
            Random rnd = new Random();
            DefaultCarConfig defaultCarConfig = rightLaneCoords[road];
            Category category = (Category)rnd.Next(0, 2);

            DrawableCar newCar = new DrawableCar(defaultCarConfig.X, defaultCarConfig.Y, defaultCarConfig.Speed, category, defaultCarConfig.State);
            lock (DrawingArea.ObjectsToDraw)
            {
                DrawingArea.ObjectsToDraw.Add(newCar);
            }
            return newCar;
        }

        public static DrawableCar CreateNewCar(int x = 0, int y = 620, int speed = 2, Category category = Category.BlueCar, State state = State.MovingRight)
        {
            DrawableCar newCar = new DrawableCar(x, y, speed, category, state);
            lock (DrawingArea.ObjectsToDraw)
            {
                DrawingArea.ObjectsToDraw.Add(newCar);
            }
            return newCar;
        }

        public static void DisposeCar(DrawableCar car)
        {
            lock (DrawingArea.ObjectsToDraw)
            {
                DrawingArea.ObjectsToDraw.Remove(car);
            }
        }

    }
}
 