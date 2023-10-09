// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

long testValue = 0;

Stopwatch stopwatch = Stopwatch.StartNew();


SemaphoreSlim semaphoreSlim = new(2, 3);

stopwatch.Start();

var t1 = Task.Run(async() =>
{
    await semaphoreSlim.WaitAsync();

    await Task.Delay(3000);

    testValue= testValue+1;
    semaphoreSlim.Release();
});

var t2 = Task.Run(async() =>
{
    await semaphoreSlim.WaitAsync();

    await Task.Delay(3000);

    testValue = testValue + 1;
    semaphoreSlim.Release();
});

var t3 = Task.Run(async() =>
{
    await semaphoreSlim.WaitAsync();

    await Task.Delay(3000);

    testValue = testValue + 1;
    semaphoreSlim.Release();
});

var a = new Task[] {t1,t2,t3 };

await Task.WhenAll(a);

stopwatch.Stop();

// Должно быть выведено 3
Console.WriteLine($"It should be 3 but got {testValue}");


Console.WriteLine(stopwatch.ElapsedMilliseconds);
