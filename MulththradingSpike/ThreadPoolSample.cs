using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MulththradingSpike
{
    internal class ThreadPoolSample
    {
        public void Execute()
        {
            ThreadPool.QueueUserWorkItem(ThreadProc);
            int workerThreads = 0;
            int completionPortThread = 0;
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThread);
            Console.WriteLine($"Available: workerThread: {workerThreads} completionPortThread: {completionPortThread}");

            //   workerThreads:
            //     The maximum number of worker threads in the thread pool.
            //
            //   completionPortThreads:
            //     The maximum number of asynchronous I/O threads in the thread pool.         
            Console.WriteLine($"Max: workerThread: {workerThreads} completionPortThread: {completionPortThread}");
            var thread = Thread.CurrentThread;
            Console.WriteLine($"Name: {thread.Name} Priority: {thread.Priority} ManagedThreadId: {thread.ManagedThreadId} IsBackground: {thread.IsBackground}: Main thread does some work, then sleeps.");
            Thread.Sleep(1000);
            Console.WriteLine("Main thread exits.");
        }

        void ThreadProc(Object? stateInfo)
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine($"Name: {thread.Name} Priority: {thread.Priority} ManagedThreadId: {thread.ManagedThreadId} IsBackground: {thread.IsBackground} : Hello from the thread pool.");
        }
    }
}
