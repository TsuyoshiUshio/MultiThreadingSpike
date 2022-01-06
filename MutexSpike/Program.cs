// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Mutex m = new Mutex(false, "MyMutex");
Console.WriteLine("Waiting for the Mutex.");
m.WaitOne();
Console.WriteLine($"{System.AppDomain.CurrentDomain.FriendlyName}: This application owns the mutex. " +
    "Press ENTER to release the mutex and exit.");
Console.ReadLine();

m.ReleaseMutex();