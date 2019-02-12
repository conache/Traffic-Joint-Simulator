using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrafficLights
{
    class Subscribable
    {
        private List<object> _subscribers = new List<object>();

        public void AddSubscriber(object subscriber)
        {
            this._subscribers.Add(subscriber);
        }

        public void NotifySubscribers()
        {
            for(int i=0; i < this._subscribers.Count; i++)
            {
                lock (this._subscribers[i])
                {
                    Monitor.Pulse(this._subscribers[i]);
                }
            }
        }
    }
}
