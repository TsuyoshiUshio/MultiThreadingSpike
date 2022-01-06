using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MulththradingSpike
{
    internal class SimpleMutex
    {
        public void Execute()
        {
            Mutex m = new Mutex(false, "MyMutex");
            Console.WriteLine("Waiting for the Mutex.");
            m.WaitOne();
            Console.WriteLine($"{System.AppDomain.CurrentDomain.FriendlyName}: This application owns the mutex. " +
                "Press ENTER to release the mutex and exit.");
            Console.ReadLine();

            m.ReleaseMutex();
        }
    }
}
