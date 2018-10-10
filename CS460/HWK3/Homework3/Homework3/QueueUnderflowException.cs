using System;

namespace Homework3
{ 
    /// <summary>
    /// Defines an exception used with the queue.
    /// </summary>
    public class QueueUnderflowException : SystemException
    {
        /// <summary>
        /// Default no-arg constructor.
        /// </summary>
        public QueueUnderflowException() : base() { }

        /// <summary>
        /// Single-argument constructor.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public QueueUnderflowException(string message) : base(message) { }
    }
}