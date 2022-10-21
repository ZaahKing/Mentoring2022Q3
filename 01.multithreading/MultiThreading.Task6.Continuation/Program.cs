/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();
            CancellationToken cancellationToken = new CancellationToken();
            var task = Task.Factory.StartNew(() =>
            {
                Console.Write("Enter: 1 - Cancel, 2 - Throw error, any - do nothing\n> ");
                var input = Console.ReadLine();
                Console.WriteLine();
                switch (input)
                {
                    case "1":
                        cancellationToken.ThrowIfCancellationRequested();
                        break;
                    case "2":
                        throw new DivideByZeroException();
                        break;
                    default:
                        Console.WriteLine("Do nothing . . .");
                        break;
                }
            }, cancellationToken);

            // Continuation task should be executed regardless of the result of the parent task.
            task.ContinueWith(
                t => { Console.WriteLine("Run anyway"); },
                TaskContinuationOptions.None);

            //Continuation task should be executed when the parent task finished without success
            task.ContinueWith(
                t => { Console.WriteLine("Parent task finished without success"); },
                TaskContinuationOptions.NotOnRanToCompletion);

            // Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
            task.ContinueWith(
                t => { Console.WriteLine(t.Exception.Message); },
                TaskContinuationOptions.OnlyOnFaulted);

            // If it succeeded.
            task.ContinueWith(
                t => { Console.WriteLine("Parent task finished"); },
                TaskContinuationOptions.OnlyOnRanToCompletion);

            // Continuation task should be executed outside of the thread pool when the parent task would be cancelled
            task.ContinueWith(
                t => { Console.WriteLine("Parent task has canseled"); },
                TaskContinuationOptions.LazyCancellation);

            task.Wait();
            Console.ReadLine();
        }
    }
}
