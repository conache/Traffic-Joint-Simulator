using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TrafficLights
{
    class TrafficLightController
    {
        private static readonly object creationLock = new object();
        private static TrafficLightController _instance = null;
        private List<TrafficLight> _trafficLights = null;
        private Thread _threadTlsUpDown = null;
        private Thread _threadTlsLeftRight = null;

        private TrafficLightController()
        {
            this._trafficLights = new List<TrafficLight>();
        }

        public static TrafficLightController Instance
        {
            get
            {
                if (_instance == null )
                {
                    lock (creationLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new TrafficLightController();
                        }
                    }
                }
                return _instance;
            }
        }

        public List<TrafficLight> TrafficLights
        {
            get
            {
                return this._trafficLights;
            }

        }

        public void RegisterTrafficLights(List<TrafficLight> trafficLights)
        {
            
           foreach ( TrafficLight trafficLight in trafficLights )
            {
                this._trafficLights.Add(trafficLight);
            }

            this.RunTrafficLightsThreads(); 
        }

        public TrafficLight GetTrafficLight(Joint.RoadLocation lane)
        {
            int laneIndex = (int)lane;
            return (laneIndex > this._trafficLights.Count) ? null : this._trafficLights[laneIndex];
        }

        private void RunTrafficLightsThreads()
        {
            this._threadTlsUpDown = 
                new Thread( () =>
                {
                    this.changeTrafficLightsState(this._trafficLights[(int)Joint.RoadLocation.Up], this._trafficLights[(int)Joint.RoadLocation.Down]);
                });
            this._threadTlsLeftRight =
                new Thread(() =>
                {
                    this.changeTrafficLightsState(this._trafficLights[(int)Joint.RoadLocation.Left], this._trafficLights[(int)Joint.RoadLocation.Right]);
                });
            this._threadTlsUpDown.Start();
            this._threadTlsLeftRight.Start();
        }

        private void changeTrafficLightsState(TrafficLight tl1, TrafficLight tl2)
        {
            while (true)
            {
                int changeTimeInterval;
                changeTimeInterval = tl1.CurrentColor == TrafficLight.Color.Yellow ? 1000 : 3000;
                Thread.Sleep(changeTimeInterval);

                tl1.ChangeToNextColor();
                tl2.ChangeToNextColor();
            }
        }
    }
}
