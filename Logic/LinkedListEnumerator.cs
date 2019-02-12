using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TrafficLights
{
    class LinkedListEnumerator<T> : IEnumerator<T>
    {
        private Node<T> _currentNode = new Node<T>();
        private Node<T> _collectionHead;

        public LinkedListEnumerator(LinkedList<T> collection)
        {
            _collectionHead = collection.First;
            _currentNode.Next = _collectionHead;
        }

        public T Current
        {
            get
            {

                return _currentNode.Value;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _currentNode = _currentNode.Next;
            return _currentNode != null;
        }

        public void Reset()
        {
            _currentNode = _collectionHead;
        }
    }
}
