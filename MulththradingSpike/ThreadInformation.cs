using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MulththradingSpike
{
    public class ThreadInformation
    {
        static object obj = new object();
        public void Execute()
        {
            ThreadPool.QueueUserWorkItem(ShowThreadInformation);
            var th1 = new Thread(ShowThreadInformation);
            th1.Start();
            var th2 = new Thread(ShowThreadInformation);
            th2.IsBackground = true;
            th2.Start();
            Thread.Sleep(500);
            ShowThreadInformation(null);
        }

        private static void ShowThreadInformation(object state)
        {
            lock (obj)
            {
                var th = Thread.CurrentThread;
                Console.WriteLine("Managed thread #{0}: ", th.ManagedThreadId);
                Console.WriteLine("  Background thread: {0}", th.IsBackground);
                Console.WriteLine("  Thread pool thread: {0}", th.IsThreadPoolThread);
                Console.WriteLine("  Priority: {0}", th.Priority);
                Console.WriteLine("  Culture: {0}", th.CurrentCulture.Name);
                Console.WriteLine("  CultureInfo: {0}", CultureInfo.CurrentCulture.Name); // The official documentation recommend to use this. 
                Console.WriteLine("  UI culture {0}", th.CurrentUICulture.Name);
                Console.WriteLine("  UI CultureInfo: {0}", CultureInfo.CurrentUICulture.Name);
                Console.WriteLine();
            }
        }
    }
}
