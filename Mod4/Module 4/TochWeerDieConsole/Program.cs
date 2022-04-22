using System.Collections.Concurrent;

namespace TochWeerDieConsole;

class Program
{
    static void Main()
    {
        //Synchronous();
        //Asynchronous();
        //Cancellen();
        //Fouten();
        //Chique();
        // MeerTakenAsync();
        //MeerDraden();
        DeGarage();

        ConcurrentBag<int> bag = new ConcurrentBag<int>();
        bag.Add(1);

        ConcurrentDictionary<string, int> dictionary = new ConcurrentDictionary<string,int>();

       

        //foreach(var nr in GetData())
        // {
        //     Console.WriteLine(nr);
        // }
        Console.WriteLine("Einde");
        Console.ReadLine();
    }

    private static void DeGarage()
    {
        Random rnd = new Random();
        Semaphore slagboom = new Semaphore(10, 10);
        Parallel.For(0, 100, idx => {
            Console.WriteLine($"Kenteken: {Thread.CurrentThread.ManagedThreadId} komt eraan");
            slagboom.WaitOne();
            Console.WriteLine($"Kenteken: {Thread.CurrentThread.ManagedThreadId} rijdt de garage in");
            Thread.Sleep(5000);
           // Task.Delay(5000).Wait();
            Console.WriteLine($"Kenteken: {Thread.CurrentThread.ManagedThreadId} rijdt de garage uit");
            slagboom.Release();
            Console.WriteLine($"Kenteken: {Thread.CurrentThread.ManagedThreadId} is buiten");
        });
    }

    static object stokje = new object();

    private static void MeerDraden()
    {
        int teller = 0;
        Parallel.For(0, 10, idx => {
            //Monitor.Enter(stokje);
            lock (stokje)
            {
                int tmp = teller;
                Task.Delay(100).Wait();
                tmp++;
                teller = tmp;
                Console.WriteLine(teller);
            }
            //Monitor.Exit(stokje);
        });
        Console.WriteLine(teller);
    }

    private static async Task MeerTakenAsync()
    {
        //Semaphore sem = new Semaphore(10, 10);
        List<Task> tasks = new List<Task>();
        for (int i = 1; i <= 1000; i++)
        {
            tasks.Add(Task.Run(() => {
                Task.Delay(i * 500).Wait();
            }));
        }
        await Task.WhenAll(tasks.ToArray());
        Console.WriteLine("Allemaal klaar");
    }

    private static IEnumerable<int> GetData()
    {
        yield return 1;
        Console.WriteLine("Na 1");
        yield return 2;
        Console.WriteLine("Na 2");
        yield return 3;
        Console.WriteLine("Na 3");
        yield return 4;
        Console.WriteLine("Na 4");
    }

    private static async void Chique()
    {
        try
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Gestart....");
                throw new Exception("Oooops");
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        //Task<int> t1 = Task.Run(() => LongAdd(3, 4));


        int result  = await LongAddAsync(5,6);
        Console.WriteLine("En verder met " + result);
    }

    private static void Fouten()
    {
        try
        {
            Task.Run(() =>
            {
                Console.WriteLine("Gestart....");
                throw new Exception("Oooops");
            }).ContinueWith(pt => { 
                if (pt.Exception != null)
                {
                    Console.WriteLine(pt.Exception.InnerException.Message);
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void Cancellen()
    {
        CancellationTokenSource nikko = new CancellationTokenSource();
        CancellationToken bommetje = nikko.Token;
        Task.Run(() => { 
            for (;;)
            {
                Console.WriteLine("Working...");
                Task.Delay(200).Wait();
                if (bommetje.IsCancellationRequested)
                {
                    Console.WriteLine("Kabooom!1");
                    return;
                }
            }
        });


        //Console.WriteLine("Press enter to stop");
        //Console.ReadLine();
        nikko.CancelAfter(5000);
    }

    private static void Asynchronous()
    {
        //Func<int, int, int> fn = LongAdd;
        //fn.BeginInvoke(3,4, ar => {
        //    int result = fn.EndInvoke(ar);
        //}, null);

        //Task<int> t1 = new Task<int>(() =>
        //{
        //    return LongAdd(3, 4);
        //});

        Task.Run<int>(() => LongAdd(3, 4))
            .ContinueWith(pt => Console.WriteLine(pt.Result));


        //Task<int> t1 = Task.Run<int>(() => LongAdd(3, 4));

        //t1.ContinueWith(pt => Console.WriteLine("T1" + pt.Result))
        //.ContinueWith(pt => Console.WriteLine("T2" + pt.Status))
        //.ContinueWith(pt => Console.WriteLine("T3" + pt.Status))
        //.ContinueWith(pt => Console.WriteLine("T4" + pt.Status));

        //t1.Start();

        //int result = t1.Result;
        //Console.WriteLine(result);

    }

    private static void Synchronous()
    {
        int res = LongAdd(2, 3);
        Console.WriteLine(res);
    }

    static int LongAdd(int a, int b)
    {
        Task.Delay(10000).Wait();
        return a + b;
    }
    static Task<int> LongAddAsync(int a, int b)
    {
        return Task.Run(() => LongAdd(a, b));
    }
}
