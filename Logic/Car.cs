using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TrafficLights
{
    class Car : Subscribable
    {
        public struct Coordinates
        {
            public Node<Slot<Car>> Slot;
            public Joint.RoadLocation Road;

            public Coordinates(Node<Slot<Car>> slot, Joint.RoadLocation road)
            {
                this.Slot = slot;
                this.Road = road;
            }
        }

        public enum MovingState
        {
            Stopped = 0,
            Moving = 1
        };

        public enum TravelState
        {
            OriginarLane = 0,
            Intersection = 1,
            DestinationLane = 2
        }

        public Thread Thread { get; }
        public MovingState MovState;
        public TravelState TravState;

        private Coordinates _coords;
        private Joint.RoadLocation DestinationRoad { get; }
        private bool _signaling;

        private readonly object _nextCarLocker = new object();
        private readonly object _trafficLightLocker = new object();
        private SimulatorUI.DrawableCar _drawableCar;

        public Node<Slot<Car>> Slot
        {
            get
            {
                return this._coords.Slot;
            }
        }

        public Coordinates Coords
        {
            get
            {
                return this._coords;
            }
        }


        public Car(Node<Slot<Car>> slot, Joint.RoadLocation road)
        {
            Random rnd = new Random();

            this._drawableCar = SimulatorUI.DrawableCarManager.CreateNewCar(road);
            this.TravState = TravelState.OriginarLane;
            this._coords = new Coordinates(slot, road);

            do
            {
                this.DestinationRoad = (Joint.RoadLocation)rnd.Next(0, 4);
            } while (this.DestinationRoad == road);


            this.Thread = new Thread(this.Drive);
            this.Thread.Start();
        }
        
        public Car getAheadCar()
        {
            Node<Slot<Car>> slotAhead = Slot.Next;

            if (slotAhead == null)
            {
                return null;
            }

            return slotAhead.Value.Item;
        }


        public bool isStopped()
        {
            return this.MovState == Car.MovingState.Stopped;
        }

        private  void Drive()
        {
            while (true)
            {
                Node<Slot<Car>> slotAhead = Slot.Next;

                if (slotAhead == null)
                {
                    if ( this.TravState == TravelState.OriginarLane )
                    {
                        // reached intersection
                        this.WaitGreenColor();
                        this.EnterIntersection();
                        continue;
                    }
                    break;
                }

                if (slotAhead.Next == null) this.StartSignaling();

                Car carAhead = getAheadCar();
                if (carAhead == null)
                {
                    this.MoveAhead();
                    this.NotifySubscribers();
                    this._drawableCar.MoveForward();
                }
                else
                {
                    this.changeState(MovingState.Stopped);
                    this.WaitFor(carAhead);
                    continue;
                }
            }

            this.Slot.Value.Item = null;
            this._drawableCar.Dispose();
        }

        private void MoveAhead()
        {
            this.changeState(MovingState.Moving);
            Node<Slot<Car>> from = this.Slot;
            Node<Slot<Car>> destination = this.Slot.Next;

            from.moveValueTo(destination, new Slot<Car>());
            this._coords.Slot = destination;
            this.changeState(MovingState.Stopped);
        }

        private void changeState( MovingState newState)
        {
            this.MovState = newState;
        }

        private void WaitFor(Subscribable observed)
        {
            lock (_nextCarLocker)
            {
                Car carAhead = this.getAheadCar();
                if ( carAhead != null && carAhead.isStopped() )
                {
                    observed.AddSubscriber(_nextCarLocker);
                    Monitor.Wait(_nextCarLocker);
                }
            }
        }

        private void StartSignaling()
        {
            Joint.RoadLocation carRoad = this.Coords.Road;

            if (this._signaling || (int)carRoad % 2 == (int)this.DestinationRoad % 2)
            {
                return;
            }

            if ( carRoad < DestinationRoad )
            {
                if (carRoad == 0 && DestinationRoad == Joint.RoadLocation.Right )
                {
                    this._drawableCar.SignalLeft();
                } else
                {
                    this._drawableCar.SignalRight();
                }
            } else
            {
                if (DestinationRoad == 0 && carRoad == Joint.RoadLocation.Left)
                {
                    this._drawableCar.SignalLeft();
                }
                else
                {
                    this._drawableCar.SignalRight();
                }
            }
            this._signaling = true;
        }

        private void WaitGreenColor()
        {
            TrafficLight trafficLight = TrafficLightController.Instance.GetTrafficLight(this.Coords.Road);
            lock (_trafficLightLocker)
            {
                while (!trafficLight.hasColor(TrafficLight.Color.Green))
                {
                    trafficLight.AddSubscriber(_trafficLightLocker);
                    Monitor.Wait(_trafficLightLocker);
                }
            }
        }

        private void EnterIntersection()
        {
            TravState = TravelState.Intersection;
            this.NotifySubscribers();
        }
    }
}
