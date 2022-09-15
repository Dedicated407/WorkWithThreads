// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Stopwatch stopWatch = new Stopwatch();
CurrentThreadFirstExample();
Console.WriteLine("------------------------------");
CurrentThreadSecondExample();
Console.WriteLine("------------------------------");
AnotherThreadExample();
Console.WriteLine("------------------------------");
ManyThreadSecondExample();
Console.WriteLine("------------------------------");
ParallelFirstExample();
Console.WriteLine("------------------------------");
ParallelSecondExample();

Console.ReadLine();

void CurrentThreadFirstExample()
{
    Console.WriteLine("Current Thread");
    DoSomething();
}

void CurrentThreadSecondExample()
{
    Stopwatch stopWatch2 = new Stopwatch();
    
    Console.WriteLine("Current Thread Second Example");
    
    stopWatch2.Start();
    CurrentThreadFirstExample();
    CurrentThreadFirstExample();
    CurrentThreadFirstExample();
    stopWatch2.Stop();
    
    Console.Write("Summary: ");
    Result(stopWatch2.Elapsed);
}

void AnotherThreadExample()
{
    Console.WriteLine("Another thread");
    Task task = new Task(DoSomething);
    task.Start();
    
    Console.ReadLine();
}

void ManyThreadSecondExample()
{
    Stopwatch stopWatch2 = new Stopwatch();
    
    Console.WriteLine("Many Thread Second Example");
    
    stopWatch2.Start();
    var tasks = new[]
    {
        Task.Factory.StartNew(DoSomething),
        Task.Factory.StartNew(DoSomething),
        Task.Factory.StartNew(DoSomething)
    };
    Task.WaitAll(tasks);
    stopWatch2.Stop();

    Console.Write("Summary: ");
    Result(stopWatch2.Elapsed);
}

void ParallelFirstExample()
{
    Console.WriteLine("Parallel");
    Parallel.Invoke(DoSomething);
}

void ParallelSecondExample()
{
    Stopwatch stopWatch2 = new Stopwatch();
    
    Console.WriteLine("Parallel Second Example");
    
    stopWatch2.Start();
    Parallel.Invoke(
        DoSomething,
        DoSomething,
        DoSomething);
    stopWatch2.Stop();
    
    Console.Write("Summary: ");
    Result(stopWatch2.Elapsed);
}

void DoSomething()
{
    stopWatch.Restart();
    for (int i = 0; i < 10; i++)
    {
        Thread.Sleep(100);
    }
    stopWatch.Stop();
    Console.WriteLine("Finish");
    Result(stopWatch.Elapsed);
}

void Result(TimeSpan ts)
{
    // Format and display the TimeSpan value.
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", 
        ts.Hours, ts.Minutes, ts.Seconds, 
        ts.Milliseconds / 10);
    Console.WriteLine("RunTime " + elapsedTime);
}