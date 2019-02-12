using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorUI
{
    class DrawableTrafficLightsManager
    {
        private struct Deviation
        {
            public int X, Y;
            
            public Deviation(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private static readonly DrawableTrafficLightsManager instance = new DrawableTrafficLightsManager();
        private static readonly Dictionary<TrafficLights.Joint.RoadLocation, Category> tlPositionMapping = new Dictionary<TrafficLights.Joint.RoadLocation, Category>()
        {
            { TrafficLights.Joint.RoadLocation.Up, Category.TrafficLightUp },
            { TrafficLights.Joint.RoadLocation.Right, Category.TrafficLightRight },
            { TrafficLights.Joint.RoadLocation.Down, Category.TrafficLightDown },
            { TrafficLights.Joint.RoadLocation.Left, Category.TrafficLightLeft },
        };
        private static readonly Dictionary<TrafficLights.TrafficLight.Color, State> tlColorMapping = new Dictionary<TrafficLights.TrafficLight.Color, State>()
        {
            { TrafficLights.TrafficLight.Color.Red, State.Red },
            { TrafficLights.TrafficLight.Color.Yellow, State.Yellow },
            { TrafficLights.TrafficLight.Color.Green, State.Green }
        };
        private static readonly Dictionary<Category, Deviation> DeviationFromCenter = new Dictionary<Category, Deviation>() {
            { Category.TrafficLightUp, new Deviation((-4) * ImageManager.Height, ImageManager.Width / 2) },
            { Category.TrafficLightLeft, new Deviation((-5) * ImageManager.Height, 11 * ImageManager.Width / 2) },
            { Category.TrafficLightDown, new Deviation(2 * ImageManager.Height, 11 * ImageManager.Width / 2) },
            { Category.TrafficLightRight, new Deviation(2 * ImageManager.Height, ImageManager.Width / 2) }
        };
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        private DrawableTrafficLightsManager()
        {
        }

        public static DrawableTrafficLightsManager Instance
        {
            get
            {
                return instance;
            }
        }

        public static DrawableTrafficLight CreateTrafficLight(TrafficLights.Joint.RoadLocation position, TrafficLights.TrafficLight.Color color)
        {
            int X = 1900;
            int y = 650;

            int centreX = X / 2;
            int centreY = y / 2;

            Category pos = tlPositionMapping[position];
            State col = tlColorMapping[color];

            DrawableTrafficLight newTrafficLight = new DrawableTrafficLight(pos, col, centreX + DeviationFromCenter[pos].X, centreY + DeviationFromCenter[pos].Y);
            
            lock (DrawingArea.ObjectsToDraw)
            {
                DrawingArea.ObjectsToDraw.Add(newTrafficLight);
            }

            return newTrafficLight;
        }
    }
}
