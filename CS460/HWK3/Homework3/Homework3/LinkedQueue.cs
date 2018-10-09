using System;

public class LinkedQueue<T> : QueueInterface<T>
{
    private Node<T> front;
    private Node<T> rear;

    public LinkedQueue()
    {
        front = null;
        rear = null;
    }

    public T Push(T element)
    {
        if (element == null)
        {
            throw new NullReferenceException();
        }

        if (IsEmpty())
        {
            Node<T> temp = new Node<T>(element, null);
            rear = front = temp;
        }
        else
        {
            /** General case */
            Node<T> temp = new Node<T>(element, null);
            rear.next = temp;
            rear = temp;
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
        else if (front == rear)
        {
            /** One item in the queue */
            temp = front.data;
            front = null;
            rear = null;
        }
        else
        {
            /** General case */
            temp = front.data;
            front = front.next;
        }

        return temp;
    }

    public bool IsEmpty()
    {
        if (front == null && rear == null)
        {
            return true;
        }

        return false;
    }
}
