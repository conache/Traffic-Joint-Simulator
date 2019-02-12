using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficLights
{
    class Slot<T>
    {
        public T Item { get; set; }

        public override string ToString()
        {
            return Item == null ? "=" : "*";
        }
    }
}
