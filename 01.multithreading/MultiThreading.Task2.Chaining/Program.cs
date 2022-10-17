/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        private static Random randomizer = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            const int ArrayLength = 10;
            
            Task.Run(() =>
            {
                var intArray = new int[ArrayLength];
                for (int i = 0; i < ArrayLength; i++)
                {
                    intArray[i] = GetRandomNumber();
                }
                PrintArray(intArray, "Step 1: Filling by random number");
                return intArray;
            })
            .ContinueWith(array =>
            {
                var intArray = array.Result;
                for (int i = 0; i < ArrayLength; i++)
                {
                    intArray[i] *= GetRandomNumber();
                }

                PrintArray(intArray, "Step 2: Multiplying by random number");
                return intArray;
            })
            .ContinueWith(array =>
            {
                var intArray = array.Result;
                Array.Sort(intArray);
                PrintArray(intArray, "Step 3: Ordering");
                return intArray;
            })
            .ContinueWith(array => Console.WriteLine("Step 4: Average number is {0}", array.Result.Average()));

            Console.ReadLine();
        }

        private static void PrintArray(int[] source, string description)
        {
            Console.WriteLine(description);
            Console.WriteLine(string.Join(", ", source));
        }

        private static int GetRandomNumber() => randomizer.Next(1000);
    }
}
