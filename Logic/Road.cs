using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    class Road
    {
        private List<Lane> _lanes;
        public enum Half
        {
            Left = 0,
            Right = 1,
        }
        public readonly TrafficLight TrafficLight;
        public List<Lane> Lanes
        {
            get
            {
                return this._lanes;
            }
        }
        public Joint.RoadLocation Location { get;  }

        public Road(Joint.RoadLocation location, TrafficLight trafficLight, int laneLength = 0)
        {
            this.Location = location;
            this.TrafficLight = trafficLight;
            this._lanes = new List<Lane> { new Lane(laneLength), new Lane(laneLength) };
        }

        public void spawnVehicle()
        {
            Lane rightLane = this.Lanes[(int)Half.Right];

            if (rightLane.acceptsVehicle())
            {
                Node<Slot<Car>> node = rightLane.Slots.First;
                Slot<Car> slot = node.Value;
                rightLane.Slots.First.Value.Item = new Car(node, this.Location);
            }
        }

        public override string ToString()
        {
            string rightLane = this.Lanes[(int)Half.Right].ToString();
            string leftLane = this.Lanes[(int)Half.Left].ToString();

            leftLane = new string(leftLane.Reverse().ToArray());

            return leftLane + "\n" + rightLane;
        }
    }
}
