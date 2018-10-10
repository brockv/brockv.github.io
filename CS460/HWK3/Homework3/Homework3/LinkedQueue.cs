using System;

namespace Homework3
{
    /// <summary>
    /// Defines a linked queue.
    /// </summary>
    /// <typeparam name="T">The element type of the linked queue.</typeparam>
    public class LinkedQueue<T> : IQueueInterface<T>
    {
        private Node<T> Front;
        private Node<T> Rear;

        /// <summary>
        /// Default no-argument constructor.
        /// </summary>
        public LinkedQueue()
        {
            Front = null;
            Rear = null;
        }

        public T Push(T element)
        {
            /** Throw an exception on a null value */
            if (element == null)
            {
                throw new NullReferenceException();
            }

            /** If the queue is empty, insert this at the beginning */
            if (IsEmpty())
            {
                Node<T> temp = new Node<T>(element, null);
                Rear = Front = temp;
            }
            /** Insert the node at the end of the linked queue */
            else
            {
                /** General case */
                Node<T> temp = new Node<T>(element, null);
                Rear.Next = temp;
                Rear = temp;
            }

            return element;
        }

        public T Pop()
        {
            T temp = default(T);

            if (IsEmpty())
            {
                throw new QueueUnderflowException("The queue was empty when pop was invoked.");
            }
            else if (Front == Rear)
            {
                /** One item in the queue */
                temp = Front.Data;
                Front = null;
                Rear = null;
            }
            else
            {
                /** General case */
                temp = Front.Data;
                Front = Front.Next;
            }

            return temp;
        }

        public bool IsEmpty()
        {
            if (Front == null && Rear == null)
            {
                return true;
            }

            return false;
        }
    }
}