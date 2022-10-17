/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private const int ItemCount = 10;
        private static readonly IList<int> CommonResource = new List<int>();
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            Task.Factory.StartNew(() =>
            {
                for (int counter = 0; counter < ItemCount; counter++)
                {

                    lock (CommonResource)
                    {
                        CommonResource.Add(counter);
                    }

                    Task.Factory.StartNew(() =>
                    {
                        lock (CommonResource)
                        {
                            foreach (int item in CommonResource)
                            {
                                Console.Write($"{item} ");
                            }
                            Console.WriteLine();
                        }
                    }).Wait();
                }
            });

            Console.ReadLine();
        }
    }
}
