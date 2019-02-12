using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TrafficLights
{
    class LinkedList<T> : IEnumerable<T>
    {
        private Node<T> _first;
        private Node<T> _last;

        public Node<T> First
        {
            get
            {
                return this._first;
            }
        }

        public Node<T> Last
        {
            get
            {
                return this._last;
            }
        }

        public void AddFirst(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (this._first == null)
            {
                this._last = newNode;
            }

            newNode.Next = this._first;
            this._first = newNode;
        }

        public void AddLast(T value)
        {
            Node<T> newNode = new Node<T>(value);
            this._last = newNode;

            if(this._first == null)
            {
                this._first = newNode;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LinkedListEnumerator<T>(this);
        }

    }
}

