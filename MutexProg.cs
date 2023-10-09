// See https://aka.ms/new-console-template for more information

long testValue = 0;

Mutex mutex = new(); //создание объекта мьютекса

var t1 = Task.Run(() => // определение метода как асинхронного
{
    mutex.WaitOne(); //ждёт освобождение мьютекса, чтобы начать выполняться
    testValue++;
    mutex.ReleaseMutex(); //освобождает мьютекс
});

var t2 = Task.Run(() =>
{
    mutex.WaitOne();
    testValue++;
    mutex.ReleaseMutex();
});

var t3 = Task.Run(() =>
{
    mutex.WaitOne();
    testValue++;
    mutex.ReleaseMutex();
});

var a = new Task[] {t1,t2,t3 };

Task.WaitAll(a); //ждёт завершение выполнения всех задач

// Должно быть выведено 3
Console.WriteLine($"It should be 3 but got {testValue}");
