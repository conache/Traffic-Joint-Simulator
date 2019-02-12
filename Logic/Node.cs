using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficLights
{
    class Node<T>
    {
        public Node<T> Next{ get; set; }
        public T Value { get; set; }

        public Node()
        {

        }

        public Node(T value)
        {
            this.Value = value;
        }

        public void moveValueTo(Node<T> destinationNode, T replacingValue)
        {
            object access = new object();

            lock (access)
            {
                destinationNode.Value = this.Value;
                this.Value = replacingValue;
            }
        }

    }
}
