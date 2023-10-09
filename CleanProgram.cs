// See https://aka.ms/new-console-template for more information

long testValue = 0;

SemaphoreSlim semaphoreSlim = new(2, 3);

var t1 = Task.Run(async() => // определение метода как асинхронного
{
    await semaphoreSlim.WaitAsync(); //ожидание завершения процесса в семафоре асинхронно и освобождения места для нового процесса
    testValue++; 
    semaphoreSlim.Release(); // освобождение места в семафоре, чтобы другой поток мог начать выполняться
});

var t2 = Task.Run(async() =>
{
    await semaphoreSlim.WaitAsync(); //ожидание завершения процесса в семафоре асинхронно и освобождения места для нового процесса
    testValue++;
    semaphoreSlim.Release(); // освобождение места в семафоре, чтобы другой поток мог начать выполняться
});

var t3 = Task.Run(async() =>
{
    await semaphoreSlim.WaitAsync(); //ожидание завершения процесса в семафоре асинхронно и освобождения места для нового процесса
    testValue++;
    semaphoreSlim.Release(); // освобождение места в семафоре, чтобы другой поток мог начать выполняться
});

var a = new Task[] {t1,t2,t3 }; 

await Task.WhenAll(a); // ожидание завершения всех тасков. Поток main будет ждать пока все таски завершатся и только тогда сделает WriteLine

// Должно быть выведено 3
Console.WriteLine($"It should be 3 but got {testValue}");
