/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        private const int ThreadCount = 10;
        private static readonly List<Thread> _threads = new List<Thread>();

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();
            Console.WriteLine("Thread");
            CreateThread(ThreadCount);
            _threads.AsParallel().ForAll(t => t.Join());
            Console.WriteLine("All threads is completed . . .");
            using(countdownEvent = new CountdownEvent(ThreadCount))
            {
                Console.WriteLine("ThreadPool");
                CreateThreadInPool(ThreadCount);
                countdownEvent.Wait();
                Console.WriteLine("All threads in pool is completed . . .");
            }
            Console.ReadLine();
        }

        private static void CreateThread(byte counter)
        {
            if (counter == 0)
            {
                return;
            }

            counter -= 1;
            Thread thread = new Thread(() => CreateThread(counter));
            lock (_threads)
            {
                _threads.Add(thread);
            }

            thread.Start();
            Thread.Sleep(3000);
            Console.WriteLine($"Thread with status {counter} is done.");
        }
        
        private static CountdownEvent countdownEvent;
        private static void CreateThreadInPool(byte counter)
        {
            if (counter == 0)
            {
                return;
            }
            counter -= 1;
            ThreadPool.QueueUserWorkItem(new WaitCallback(x =>
            {
                CreateThreadInPool(counter);
                Thread.Sleep(3000);
                Console.WriteLine($"Thread with status {counter} is done.");
                countdownEvent.Signal();
            }), counter);
        }
    }
}
