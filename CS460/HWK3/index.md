# **Homework II**

The task for this assignment was to learn the C# language by translating a Java application into a C# console application, using Visual Studio for our IDE. Having a strong background in both C++ and Java coming into this sequence, this particular assignment was straightforward and fairly simple.

## **Relevant Links**
- [Home](https://brockv.github.io/)
- [Assignment Page](http://www.wou.edu/~morses/classes/cs46x/assignments/HW3.html)
- [Code Repository](https://github.com/brockv/brockv.github.io/tree/master/CS460/HWK3/Homework3/demo)

### **I: Setting Up the Environment and Examining the Existing Project**

This step was halfway done as I already had Visual Studio setup on both my desktop and laptop from previous projects. Because of this, I was able to spend more time looking through the code we were given, and learning how each file worked. The project we were provided with accepted a single integer from the user at the command line and produced binary representations of numbers up to the one provided, starting from 1.


### **II: Starting the Conversion to C#**

As was suggested in the homework assignment, I started with Node.js:

# ****Node.js --> Node.cs ****

There weren't many changes to make to this file other than minor changes to capitalization.

```java
```

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

# ****QueueInterface.js --> QueueInterface.cs****

As with the previous file, there wasn't much to do here other than adjusting names to match C# standards.

```java
```

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

# ****QueueUnderflowException.js --> QueueUnderflowException.cs****

The major change in this file was moving from using 'super' to 'base', and restructuring the method signatures to match standards.

```java
```

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

# ****LinkedQueue.js --> LinkedQueue.cs****

```java
```

```c#
```

# ****Main.js --> ProgramDriver.cs****

```java
```

```c#
```

### **V: Making Sure Everything Works**



### **VI: Merging Back Into the Master Branch**

The last step in all of this was to merge the feature branch back into the main branch. In order to do that, I first needed to switch back to the master branch, then merge the feature branch into it. After a final commit, I used the following commands to accomplish that:

```bash
git checkout master
git merge HWK3-feature-branch
git push -u origin master
```