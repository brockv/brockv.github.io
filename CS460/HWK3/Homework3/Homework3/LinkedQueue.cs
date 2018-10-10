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

        /// <summary>
        /// Inserts a given node into a linked list.
        /// </summary>
        /// <param name="element">The value to assign the node.</param>
        /// <returns>The value of the node.</returns>
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

        /// <summary>
        /// Removes a node from a linked list.
        /// </summary>
        /// <returns>The value of the last node in the list.</returns>
        public T Pop()
        {
            T temp = default(T);

            /** If the queue is empty, report to the user and return immediately */
            if (IsEmpty())
            {
                throw new QueueUnderflowException("The queue was empty when pop was invoked.");
            }
            /** One item in the queue */
            else if (Front == Rear)
            {                
                temp = Front.Data;
                Front = null;
                Rear = null;
            }
            /** General case */
            else
            {                
                temp = Front.Data;
                Front = Front.Next;
            }

            return temp;
        }

        /// <summary>
        /// Checks if a queue contains any nodes.
        /// </summary>
        /// <returns>True if there are no nodes; false otherwise.</returns>
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