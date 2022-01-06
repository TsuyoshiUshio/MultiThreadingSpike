using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MulththradingSpike
{
    internal class MutexThreads
    {
        private static Mutex mut;  // original sample is = new Mutex(true); however, it through AbandanedMutexException is thrown.
        private const int numIterations = 1;
        private const int numThreads = 3;
        public static void Execute()
        {
             bool isCreated = false;
            mut = new Mutex(true);  // Local mutex
            //mut = new Mutex(true, "TsuyoshiMutex", out isCreated); // sytem mutex. this works too. 
            Console.WriteLine($"isCreated: {isCreated}");
            for (int i = 0; i < numThreads; i++)
            {
                Thread myThread = new Thread(new ThreadStart(MyThreadProc));
                myThread.Name = String.Format("Thread{0}", i + 1);
                myThread.Start();
            }

            Console.WriteLine("Creating thread owns the Mutex.");
            Thread.Sleep(1000);
            Console.WriteLine("Creating thread releases the Mutex. \r\n");
            mut.ReleaseMutex();
            Console.ReadLine();
        }

        private static void MyThreadProc()
        {
            for (int i = 0;i < numIterations; i++)
            {
                UseResource();
            }

        }

        private static void UseResource()
        {
            mut.WaitOne();
            Console.WriteLine("{0} has entered the protected area", Thread.CurrentThread.Name);
            Thread.Sleep(500);
            Console.WriteLine("{0} is leaving the protected are \r\n", Thread.CurrentThread.Name);
            mut.ReleaseMutex();
        }
    }
}
