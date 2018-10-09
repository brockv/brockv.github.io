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
        static LinkedList<string> GenerateBinaryRepresentationList(int n)
        {
            /** Create an empty queue of strings with which to perform the traversal */
            LinkedQueue<StringBuilder> theQueue = new LinkedQueue<StringBuilder>();

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
            theQueue.Push(new StringBuilder("1"));

            /** BFS */
            while (n-- > 0)
            {
                /** Print the front of the queue */
                StringBuilder sb = theQueue.Pop();
                output.AddLast(sb.ToString());

                /** Make a copy */
                StringBuilder sbc = new StringBuilder(sb.ToString());

                /** Left child */
                sb.Append('0');
                theQueue.Push(sb);

                /** Right child */
                sbc.Append('1');
                theQueue.Push(sbc);
            }

            return output;
        }

        /** Driver program to test the above function */
        static void Main(string[] args)
        {
            /**
             * Show how to invoke the application if the parameter is missing
             */
            int n = 10;
            if (args.Length < 1)
            {
                Console.WriteLine("Please invoke with the max value to print binary up to, like this:");
                Console.WriteLine("\t Main 12");
                return;
            }

            /**
              * Handle the command line arguments
              */
            try
            {
                /**
                  * Attempt to parse the input passed in
                  */
                n = int.Parse(args[0]);

                /** Don't attempt to process negative numbers */
                if (n < 0)
                {
                    Console.WriteLine("\nPlease use non-negative integers.\n");
                    return;
                }
            }
            catch (FormatException e)
            {
                /** Don't attempt to process non numeric input */
                Console.WriteLine("\nI'm sorry, I can't understand the number: " + args[0] + "\n");
                return;
            }

            /** Generate the output */
            LinkedList<string> output = GenerateBinaryRepresentationList(n);

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

        }
    }
}