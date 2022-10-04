﻿/*
 * 1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.
 * Each Task should iterate from 1 to 1000 and print into the console the following string:
 * “Task #0 – {iteration number}”.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task1._100Tasks
{
    class Program
    {
        const int TaskAmount = 100;
        const int MaxIterationsCount = 1000;

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. Multi threading V1.");
            Console.WriteLine("1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.");
            Console.WriteLine("Each Task should iterate from 1 to 1000 and print into the console the following string:");
            Console.WriteLine("“Task #0 – {iteration number}”.");
            Console.WriteLine();
            
            HundredTasks();

            Console.ReadLine();
        }

        static void HundredTasks()
        {
            // ParallelCountingViaTasks();
            ParallelCountingViaThreads();
        }

        private static void ParallelCountingViaThreads()
        {
            var threads = Enumerable.Range(0, TaskAmount)
                .Select(x => new Thread(() => CountToThousand(x)))
                .ToArray();
            Parallel.ForEach(threads, thread => thread.Start());
            Parallel.ForEach(threads, thread => thread.Join());
        }

        private static void ParallelCountingViaTasks()
        {
            var tasks = Enumerable.Range(0, TaskAmount)
                .Select(x => new Task(() => CountToThousand(x)))
                .ToArray();
            Parallel.ForEach(tasks, task => task.Start());
            Task.WaitAll(tasks);
        }

        static void CountToThousand(int taskNumber)
        {
            for (int iterationNumber = 0; iterationNumber < MaxIterationsCount; iterationNumber++)
            {
                Output(taskNumber, iterationNumber);
            }
        }

        static void Output(int taskNumber, int iterationNumber)
        {
            Console.WriteLine($"Task #{taskNumber} – {iterationNumber}");
        }
    }
}
