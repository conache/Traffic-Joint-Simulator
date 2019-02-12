using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficLights
{
    class Lane
    {

        private LinkedList<Slot<Car>> _slots;
        private int _length;

        public static int DEAFAULT_LENGTH = 12;

        public int Length
        {
            get
            {
                return this._length;
            }
        }

        public LinkedList<Slot<Car>> Slots
        {
            get
            {
                return this._slots;
            }
        }

        public Lane (int length = 0)
        {
            this._length = length != 0 ? length : Lane.DEAFAULT_LENGTH;
            this._slots = new LinkedList<Slot<Car>>();

            for(int i = 0; i < this.Length; i++)
            {
                this.Slots.AddFirst(new Slot<Car>());
            }
        }

        public bool acceptsVehicle()
        {
            return this.Slots.First.Value.Item == null;
        }

        public override string ToString()
        {
            string lane = "";
            foreach (Slot<Car> slot in this.Slots)
            {
                lane += slot;
            }

            return lane;
        }
    }
}
