using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MulththradingSpike
{
    public class ThreadStatic
    {
        [ThreadStatic] static double previous = 0.0;
        [ThreadStatic] static double sum = 0.0;
        [ThreadStatic] static int calls = 0;
        [ThreadStatic] static bool abnormal;
        static int totalNumbers = 0;
        static CountdownEvent countdown;
        private static Object lockObj;
        Random rand;

        public ThreadStatic()
        {
            rand = new Random();
            lockObj = new Object();
            countdown = new CountdownEvent(1);

        }

        public void Execute()
        {
            var threadStatic = new ThreadStatic();
            Thread.CurrentThread.Name = "Main";
            threadStatic.Process();
            countdown.Wait();
            Console.WriteLine("{0:N0} random numbers were generated.", totalNumbers);
        }

        private void Process()
        {
            for (int threads = 1; threads <= 10; threads++)
            {
                var newThread = new Thread(new ThreadStart(this.GetRandomNumbers));
                countdown.AddCount();
                newThread.Name = threads.ToString();
                newThread.Start();
            }
            this.GetRandomNumbers();
        }
        private void GetRandomNumbers()
        {
            double result = 0.0;
            for (int ctr = 0; ctr < 20000000; ctr++)
            {
                lock(lockObj)
                {
                    result = rand.NextDouble();
                    calls++;
                    Interlocked.Increment(ref totalNumbers);
                    // We should never get the same random number twice.
                    if (result == previous)
                    {
                        abnormal = true;
                        break;
                    } else
                    {
                        previous = result;
                        sum += result;
                    }
                }
            }
            if (abnormal)
                Console.WriteLine($"Result is {previous} in {Thread.CurrentThread.Name}");

            Console.WriteLine($"Thread {Thread.CurrentThread.Name} finished random number generation.");
            Console.WriteLine("Sum = {0:N4}, Mean = {1:N4}, n = {2:N0}\n", sum, sum / calls, calls);
            countdown.Signal();
        }
    }
}
