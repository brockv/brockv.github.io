# **Homework III**

The task for this assignment was to learn the C# language by translating a Java application into a C# console application, using Visual Studio for our IDE. Having a strong background in both C++ and Java coming into this sequence, this particular assignment was straightforward and fairly simple.

## **Relevant Links**
- [Home](https://brockv.github.io/)
- [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW3.html)
- [Code Repository](https://github.com/brockv/brockv.github.io/tree/master/CS460/HWK3/Homework3/demo)

### **I: Setting Up the Environment and Examining the Existing Project**

This step was halfway done as I already had Visual Studio setup on both my desktop and laptop from previous projects. Because of this, I was able to spend more time looking through the code we were given, and learning how each file worked. The project we were provided with accepted a single integer from the user at the command line and produced binary representations of numbers up to the one provided, starting from 1.


### **II: Starting the Conversion to C#**

As was suggested in the homework assignment, I started with Node, then QueueInterface, QueueUnderflowException, LinkedQueue, and finally Main.

### **Node.java --> Node.cs**

There weren't many changes to make to this file other than minor changes to capitalization.


```c#
namespace Homework3
{
    /// <summary>
    /// Singly-linked node class.
    /// </summary>
    /// <typeparam name="T">The element type of the node.</typeparam>
    public class Node<T>
    {
        public T Data;
        public Node<T> Next;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The contents of the node.</param>
        /// <param name="next">The node this one points to.</param>
        public Node(T data, Node<T> next)
        {
            this.Data = data;
            this.Next = next;
        }
    }
}
```

Next up was QueueInterface.js:

### **QueueInterface.java --> QueueInterface.cs**

As with the previous file, there wasn't much to do here other than adjusting names to match C# standards.


```c#
using System;

namespace Homework3
{
    /// <summary>
    /// A FIFO queue interface. This ADT is suitable for a singly-linked
    /// queue.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public interface IQueueInterface<T>
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
}
```

### **QueueUnderflowException.java --> QueueUnderflowException.cs**

The major change in this file was moving from using 'super' to 'base', and restructuring the method signatures to match standards.


```c#
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
```

### **LinkedQueue.java --> LinkedQueue.cs**

Other than adding XML comments, there wasn't much to change in this file.

```c#
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
```

### **Main.java --> ProgramDriver.cs**

Other than adding XML style comments, the only other significant change I made was a check for negative numbers and preventing them from being processed.

```c#
using System;
using System.Collections.Generic;
using System.Text;

namespace Homework3
{
    /**
     * Print the binary representation of all numbers from 1 up to n.
     * This is accomplished by using a FIFO queue to perform a level 
     * order (i.e. BFS) traversal of a virtual binary tree that 
     * looks like this:
     *                 1
     *             /       \
     *            10       11
     *           /  \     /  \
     *         100  101  110  111
     *          etc.
     * and then storing each "value" in a list as it is "visited".
     */
    class ProgramDriver
    {
        /// <summary>
        /// Generates a linked list of binary representations of integers.
        /// </summary>
        /// <param name="n">The number of integers to convert.</param>
        /// <returns>A linked list of the converted integers.</returns>
        static LinkedList<string> GenerateBinaryRepresentationList(int n)
        {
            /** Create an empty queue of strings with which to perform the traversal */
            LinkedQueue<StringBuilder> TheQueue = new LinkedQueue<StringBuilder>();

            /** A list for returning the binary values */
            LinkedList<string> output = new LinkedList<string>();

            if (n < 1)
            {
                /** Binary representation of negative values is not supported. Return
                 *  an empty list.
                 */
                return output;
            }

            /** Enqueue the first binary number. Use a dynamic string to avoid string concat */
            TheQueue.Push(new StringBuilder("1"));

            /** BFS */
            while (n-- > 0)
            {
                /** Print the front of the queue */
                StringBuilder sBuilder = TheQueue.Pop();
                output.AddLast(sBuilder.ToString());

                /** Make a copy */
                StringBuilder sBuilderCopy = new StringBuilder(sBuilder.ToString());

                /** Left child */
                sBuilder.Append('0');
                TheQueue.Push(sBuilder);

                /** Right child */
                sBuilderCopy.Append('1');
                TheQueue.Push(sBuilderCopy);
            }

            return output;
        }

        /** Driver program to test the above function */
        static void Main(string[] args)
        {
            /** Show how to invoke the application if the parameter is missing */
            int n = 10;
            if (args.Length < 1)
            {
                Console.WriteLine("Please invoke with the max value to print binary up to, like this:");
                Console.WriteLine("\t Homework3 10");
                return;
            }

            /** Handle the command line arguments */
            try
            {
                /** Attempt to parse the input passed in */
                n = int.Parse(args[0]);

                /** Don't attempt to process negative numbers */
                if (n < 0)
                {
                    Console.WriteLine("\nPlease use non-negative integers.\n");
                    return;
                }
            }
            catch (FormatException)
            {
                /** Don't attempt to process non numeric input */
                Console.WriteLine("\nI'm sorry, I can't understand the number: " + args[0] + "\n");
                return;
            }

            /** Generate the output */
            LinkedList<string> output = GenerateBinaryRepresentationList(n);

            /** Whitespace for improved readability */
            Console.WriteLine();

            /** 
             * Print the output right justified. Longest string is the last one. Make sure to
             * print enough spaces to move it over the correct distance.
             */
            int maxLength = output.Last.Value.Length;
            foreach (string s in output)
            {
                for (int i = 0; i < maxLength - s.Length; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(s);
            }

            /** Whitespace for improved readability */
            Console.WriteLine();
        }
    }
}
```

### **III: Making Sure Everything Works**

Once all of the files had been translated into C#, it was time to build and test it out.

![](images/test_one.PNG?raw=true)

![](images/test_two.PNG?raw=true)

![](images/test_three.PNG?raw=true)

Everything worked! One more pass through the files to make sure my comments were sufficient and it was good to be pushed to my remote repository.

### **IV: Merging Back Into the Master Branch**

The last step in all of this was to merge the feature branch back into the main branch, and it's done the same way as in the previous assignment. Move back to master, merge the feature branch in, then push to the repository.

```bash
git checkout master
git merge HWK3-feature-branch
git push -u origin master
```
