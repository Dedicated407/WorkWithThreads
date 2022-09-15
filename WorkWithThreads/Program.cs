// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Stopwatch stopWatch = new Stopwatch();
OneThreadFirstExample();
Console.WriteLine("------------------------------");
ParallelFirstExample();
Console.WriteLine("------------------------------");
OneThreadSecondExample();
Console.WriteLine("------------------------------");
ParallelSecondExample();

Console.ReadLine();

void OneThreadFirstExample()
{
    Console.WriteLine("OneThread");
    stopWatch.Restart();
    for (var i = 0; i < 10; i++)
    {
        DoWork(i);
    }
    stopWatch.Stop();
    Result(stopWatch.Elapsed);
}

void OneThreadSecondExample()
{
    Stopwatch stopWatch2 = new Stopwatch();
    
    Console.WriteLine("One Thread Second Example");
    
    stopWatch2.Start();
    OneThreadFirstExample();
    OneThreadFirstExample();
    OneThreadFirstExample();
    stopWatch2.Stop();
    
    Console.Write("Summary: ");
    Result(stopWatch2.Elapsed);
}

void ParallelFirstExample()
{
    Console.WriteLine("Parallel");
    stopWatch.Restart();
    Parallel.For(0, 10, DoWork);
    stopWatch.Stop();
    Result(stopWatch.Elapsed);
}

void ParallelSecondExample()
{
    Stopwatch stopWatch2 = new Stopwatch();
    
    Console.WriteLine("Parallel Second Example");
    
    stopWatch2.Start();
    Parallel.Invoke(
        OneThreadFirstExample,
        OneThreadFirstExample,
        OneThreadFirstExample);
    stopWatch2.Stop();
    
    Console.Write("Summary: ");
    Result(stopWatch2.Elapsed);
}

void DoWork(int i)
{
    Thread.Sleep(100);
}

void Result(TimeSpan ts)
{
    // Format and display the TimeSpan value.
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", 
        ts.Hours, ts.Minutes, ts.Seconds, 
        ts.Milliseconds / 10);
    Console.WriteLine("RunTime " + elapsedTime);
}