using System;

/// <summary>
/// A FIFO queue interface. This ADT is suitable for a singly-linked
/// queue.
/// </summary>
/// <typeparam name="T">The element type.</typeparam>
public interface QueueInterface<T>
{
    /// <summary>
    /// Add an element to the rear of the queue
    /// </summary>
    /// <param name="element">The element to add</param>
    /// <returns> The element that was enqueued</returns>
    T Push(T element);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    T Pop();
    
    /// <summary>
    /// Test if the queue is empty
    /// </summary>
    /// <returns>True if the queue is empty; otherwise false</returns>
    bool IsEmpty();
}
