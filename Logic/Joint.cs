using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TrafficLights
{
    internal class Joint
    {
        private static int DEFAULT_ROADS_NUMBER = 4;
        private List<Road> _roads = new List<Road>();

        public List<Road> Roads
        {
            get
            {
                return this._roads;
            }

        }
        public enum RoadLocation
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        };

        public Joint()
        {
            List<TrafficLight> trafficLights = 
                new List<TrafficLight>
                    {
                        new TrafficLight(RoadLocation.Up, TrafficLight.Color.Red),
                        new TrafficLight(RoadLocation.Right),
                        new TrafficLight(RoadLocation.Down, TrafficLight.Color.Red),
                        new TrafficLight(RoadLocation.Left)
                    };
            TrafficLightController.Instance.RegisterTrafficLights(trafficLights);

            this.Roads.Add(new Road(RoadLocation.Up, trafficLights[(int)RoadLocation.Up], 6));
            this.Roads.Add(new Road(RoadLocation.Right, trafficLights[(int)RoadLocation.Right]));
            this.Roads.Add(new Road(RoadLocation.Down, trafficLights[(int)RoadLocation.Down], 8));
            this.Roads.Add(new Road(RoadLocation.Left, trafficLights[(int)RoadLocation.Left]));

        }
    }
}
